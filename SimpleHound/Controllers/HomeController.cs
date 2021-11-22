using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleHound.Models;
using Microsoft.AspNetCore.Authorization;
using SimpleHound.SQLDatabase;
using SimpleHound.MyLists;
using SimpleHound.Logic;


namespace SimpleHound.Controllers
{

    public class HomeController : Controller
    {
        private readonly MenuDBContext _context;

        public HomeController(MenuDBContext context)
        {
            _context = context;
        }

        public static FoodLists FoodLists = new FoodLists();



        public IActionResult Index()
        {
            //If this user has menu items in databse, direct user to Dashboard page
            //All new users are sent to MenuTableEntry page
           
                var containsUser = _context.MenuEntry.Where(x => x.UserName == User.Identity.Name).ToList();

                if (containsUser.Count > 0)
                {
                    return RedirectToAction("HomeDashboard", "Dashboard");
                }
                if(User.Identity.Name != null)
                {
                    return RedirectToAction("MenuTableEntry", "Home");
                }

                return View();
            
        }

        //------------------------------------------ Table Entry --------------------------------------
        [Authorize]
        [HttpGet]
        public IActionResult MenuTableEntry()
        {
            //If the users login info already exists in the database, direct user to Dashboard page
           
                var containsUser = _context.MenuEntry.Where(x => x.UserName == User.Identity.Name).ToList();

                if (containsUser.Count > 0)
                {
                    return RedirectToAction("HomeDashboard", "Dashboard");
                }

                return View();
            
        }

        [HttpPost]
        public IActionResult MenuTableEntry(string dropTable)
        {

            int selectedTable = int.Parse(dropTable);


            if (dropTable != null)
            {
                return RedirectToAction("MenuFoodEntry", new { id = selectedTable });
            }
            return View();
        }

        //----------------------------------- Food Entry ---------------------------------------------

        [HttpGet]
        public IActionResult MenuFoodEntry(string id = "")
        {
            ViewData["tableNum"] = id;


          
                FoodLists.FoodItemList.Clear();
                FoodLists.CategoryList.Clear();

                //When existing user logs in, the menu information will be retrieved

                var containsUser = _context.MenuEntry.Where(x => x.UserName == User.Identity.Name);

                //adds food items to two lists
                FoodLists = Helper.AddCategoryandFoodItems(containsUser, FoodLists);
               
            //deletes any duplicate categories and adds the new list with no repeating categories

            List<string> distinctCategory = FoodLists.CategoryList.Distinct().ToList();
            FoodLists.CategoryList.Clear();

            FoodLists.CategoryList.AddRange(distinctCategory);

            return View(FoodLists);
        }


        [HttpPost]
        public IActionResult MenuFoodEntry(string EntryBoxCategory, string CategoryItem, string EntryBoxItem, string EntryBoxPrice, string addCategory, string addFoodItem, string deleteFoodItem, string deleteCategory, string SubmitMenu, int id)
        {
            int.TryParse(deleteFoodItem, out int deleteItem);
            int.TryParse(deleteCategory, out int deleteNum);
            string categoryError = "";
            string foodItemError = "";


            categoryError = Helper.CategoryErrorMessage(addCategory, EntryBoxCategory);
            ViewData["categoryError"] = categoryError;

            foodItemError = Helper.FoodItemError(addFoodItem, EntryBoxItem, EntryBoxPrice);
            ViewData["foodItemError"] = foodItemError;


            if (deleteCategory != null)
            {
               
                foreach (var item in FoodLists.FoodItemList.ToList())
                {
                    Helper.DeleteFoodItemsInsideCategory(FoodLists, deleteNum, item);
                    
                }
                FoodLists.CategoryList.RemoveAt(deleteNum);

            }
            else if (deleteFoodItem != null)
            {
                FoodLists.FoodItemList.RemoveAt(deleteItem);
            }
            else
            {
                if (addCategory != null && categoryError == "")
                {
                    FoodLists.CategoryList.Insert(0, EntryBoxCategory);
                }
                else
                {
                    if (addFoodItem != null)
                    {
                        //TableNum is 0 every time you try to go back from Dashboard to edit the menu info.
                        //This Fix retrieves the database tableNum only when table count is Equal to zero.

                        int tableNum = id;
                        if (tableNum == 0)
                        {
                           
                                var fixZero = _context.MenuEntry.Where(x => x.UserName == User.Identity.Name);

                               tableNum = Helper.RetreaveDatabaseTableNum(fixZero, tableNum);                              
                            
                        }
                        if (foodItemError == "")
                        {
                            //adding all items to the list
                            decimal.TryParse(EntryBoxPrice, out decimal priceNum);
                            FoodLists.FoodItemList.Add(new MenuEntry { UserName = User.Identity.Name, Tables = tableNum, Categories = CategoryItem, Items = EntryBoxItem, Prices = priceNum });

                        }

                    }
                    if (SubmitMenu == "Next")
                    {

                        if (FoodLists.FoodItemList.Count == 0)
                        {
                            ViewData["ErrorNext"] = "*You need to add at least one item*";
                        }
                        else
                        {
                           
                                //delete existing database info
                                _context.MenuEntry.RemoveRange(_context.MenuEntry.Where(x => x.UserName == User.Identity.Name));

                                //Add updated list to database
                                _context.MenuEntry.AddRange(
                                    FoodLists.FoodItemList
                                    );
                                _context.SaveChanges();
                            
                            return RedirectToAction("HomeDashboard", "Dashboard");
                        }


                    }

                }

            }


            return View(FoodLists);
        }
        //--------------------------------------------------------------------------------






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
