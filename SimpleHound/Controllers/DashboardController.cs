using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleHound.SQLDatabase;

namespace SimpleHound.Controllers
{


    public class DashboardController : Controller
    {
        private readonly MenuDBContext _context;

        public DashboardController(MenuDBContext context)
        {
          _context = context;
        }

        public static List<MenuEmployees> EmployeeList = new List<MenuEmployees>();


        public IActionResult HomeDashboard(int id)
        {
            ViewData["TableNum"] = id;
            return View();
        }

        //--------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult AddEmployees()
        {
           
           
               EmployeeList.Clear();
                var contentUser = _context.MenuEmployees.Where(x => x.UserName == User.Identity.Name);
                foreach (var item in contentUser)
                {
                    EmployeeList.Add(new MenuEmployees { UserName = item.UserName, Position = item.Position, Name = item.Name, Password = item.Password });
                }
            

            return View(EmployeeList);
        }

        [HttpPost]
        public IActionResult AddEmployees(string EmployeeName, string DeleteItem, string position, string addWaiter)
        {
            if (addWaiter == "waiter" && EmployeeName == null || addWaiter == "waiter" && position == null)
            {
                ViewData["StaffError"] = "* All fields must be filled *";
            }
            else
            {
                //Deletes from database and list

                if (DeleteItem != null)
                {
                    int.TryParse(DeleteItem, out int deleteNum);

                   
                        _context.MenuEmployees.RemoveRange(_context.MenuEmployees.Where(x => x.Password == EmployeeList[deleteNum].Password));

                        _context.SaveChanges();
                    

                    EmployeeList.RemoveAt(deleteNum);
                }
                else
                {
                    Random r = new Random();
                    int randomPassword = r.Next(1000, 9000);

                    string myPass = EmployeeName + randomPassword.ToString();

                    MenuEmployees theEmployee = new MenuEmployees() { UserName = User.Identity.Name, Position = position, Name = EmployeeName, Password = myPass.ToUpper() };
                    EmployeeList.Insert(0, theEmployee);


                  
                        _context.MenuEmployees.Add(
                            theEmployee);

                        _context.SaveChanges();
                    

                }
            }



            return View(EmployeeList);
        }

        //----------------------------------------------------------------------------------------------------
        

       

    }
}