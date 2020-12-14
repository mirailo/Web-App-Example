using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Storage;
using MyFirstRazorWebPage.Models;
using MyFirstRazorWebPage.Pages.DatabaseConnection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace MyFirstRazorWebPage.Pages.Customers
{
    public class CustomerLoginModel : PageModel
    {
        private readonly HairSalonApptContext _context;

        public CustomerLoginModel(HairSalonApptContext context)
        {
            _context = context;
        }


        public IActionResult OnGet()
        {
            return Page();
        }

        [Required]
        [BindProperty]
        public Customer Customer { get; set; }

        public string SessionID;

        [BindProperty]
        public string UserName { get; set; }

        [BindProperty]
        public string UserEmail { get; set; }


        [BindProperty]
        public string Msg { get; set; }
        

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            DatabaseConnect DBCon = new DatabaseConnect(); 
            string dbStringConnection = DBCon.DBStringConnection();

            connectionStringBuilder.DataSource = dbStringConnection;
            var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);

            connection.Open();

            var selectCmd = connection.CreateCommand();
            selectCmd.CommandText = @"SELECT Password FROM Customer WHERE EmailAdd=$EmailAdd";
            selectCmd.Parameters.AddWithValue("$EmailAdd", Customer.EmailAdd);

            var reader = selectCmd.ExecuteReader();
            Console.WriteLine("Before Password");
            var Pwd = "";
            Console.WriteLine("Password is " + Pwd);

            while (reader.Read())
            {
                Pwd = reader.GetString(0);
            }


            if (Customer.Password.Equals(Pwd))
                    {
                        selectCmd = connection.CreateCommand();
                        selectCmd.CommandText = @"SELECT FirstName FROM Customer WHERE EmailAdd=$EmailAdd";
                        selectCmd.Parameters.AddWithValue("$EmailAdd", Customer.EmailAdd);
                        var reader2 = selectCmd.ExecuteReader();

                        while (reader2.Read())
                        {
                            UserName = reader2.GetString(0);
                        }

                        SessionID = HttpContext.Session.Id; //set the variable as session ID to allow multiple session in 1 browser
                        DateTime dd = DateTime.Now;
                        int hour = dd.Hour;
                        int min = dd.Minute;
                        int month = dd.Month;
                        int day = dd.Day;

                        string dateTime = day + "," + month + "," + hour + "," + min;
                        HttpContext.Session.SetString("sessionID", SessionID);
                        Console.WriteLine("1 - session ID : "+SessionID);

                        //checking the user has multiple session or not
                        var selectCmd4 = connection.CreateCommand();
                        selectCmd4 = connection.CreateCommand();
                        selectCmd4.CommandText = @"SELECT Username, SessionID, DateTime FROM UserSession WHERE SessionID=$sessionID";
                        selectCmd4.Parameters.AddWithValue("$sessionID", SessionID);
                        var reader4 = selectCmd4.ExecuteReader();

                        string[] SessionCheck = new string[3];
                        while (reader4.Read())
                        {
                            SessionCheck[0] = reader4.GetString(0); //session Username
                            SessionCheck[1] = reader4.GetString(1); //session ID
                            SessionCheck[2] = reader4.GetString(2); //session Date time
                        }
                         Console.WriteLine("SessionCheck[1] : "+SessionCheck[1]);

                        if (SessionCheck[1]== SessionID && SessionCheck[0]==UserName) // checking if the session ID and username are in DB
                        {

                            string[] getDateTime = SessionCheck[2].Split(","); //day month hour min

                            Console.WriteLine("Day :"+ getDateTime[0]);
                            Console.WriteLine("Month : "+ getDateTime[1]);

                            if (Convert.ToInt32(getDateTime[0])==day && Convert.ToInt32(getDateTime[1]) == month)//check same month and day
                            {
                                int HourDiff = hour-Convert.ToInt32(getDateTime[2]);
                                int MinDiff = min-Convert.ToInt32(getDateTime[3]);
                                Console.WriteLine("Hour diff : "+HourDiff);
                                Console.WriteLine("Min diff : "+MinDiff);


                                if (HourDiff > 0 || MinDiff > 20) //session obselete
                                {
                                    //Delete record and create a new login
                                    var selectCmd2 = connection.CreateCommand();
                                    selectCmd2.CommandText = @"DELETE FROM UserSession WHERE Username=$userName";
                                    selectCmd2.Parameters.AddWithValue("$userName", UserName);
                                    selectCmd2.Prepare();
                                    selectCmd2.ExecuteNonQuery();

                                    Console.WriteLine("A session record deleted");
                                     //saving the session to Db
                                    var selectCmd3 = connection.CreateCommand();
                                    selectCmd3.CommandText = @"INSERT INTO UserSession (Username, SessionID, DateTime) VALUES ($username, $sessionID, $dateTime)";
                                    selectCmd3.Parameters.AddWithValue("$username", UserName);
                                    selectCmd3.Parameters.AddWithValue("$sessionID", SessionID);
                                    selectCmd3.Parameters.AddWithValue("$dateTime", dateTime);

                                    selectCmd3.Prepare();
                                    selectCmd3.ExecuteNonQuery();

                                    return RedirectToPage("/CustomerLoggedIn/SuccessLogin");
                                }
                                else //user has an active session yet
                                {
                                    Msg = "Multilple Session is not allowed! Wait after 20 minutes before logon for a security reason.";
                                    Console.WriteLine(Msg);
                     
                                    return Page();
                                }     
                            }
                            else //session obselete : more than 1 day
                            {

                                    HttpContext.Session.SetString("username", UserName);
                                    HttpContext.Session.SetString("email", Customer.EmailAdd);
                                    HttpContext.Session.SetString("sessionID", SessionID);

                                //Delete record and create a new login
                                    var selectCmd2 = connection.CreateCommand();
                                    selectCmd2.CommandText = @"DELETE FROM UserSession WHERE Username=$userName";
                                    selectCmd2.Parameters.AddWithValue("$userName", UserName);
                                    selectCmd2.Prepare();
                                    selectCmd2.ExecuteNonQuery();


                                 //saving the session to Db
                                    var selectCmd3 = connection.CreateCommand();
                                    selectCmd3.CommandText = @"INSERT INTO UserSession (Username, SessionID, DateTime) VALUES ($username, $sessionID, $dateTime)";
                                    selectCmd3.Parameters.AddWithValue("$username", UserName);
                                    selectCmd3.Parameters.AddWithValue("$sessionID", SessionID);
                                    selectCmd3.Parameters.AddWithValue("$dateTime", dateTime);

                                    selectCmd3.Prepare();
                                    selectCmd3.ExecuteNonQuery();

                                    

                                    return RedirectToPage("/CustomerLoggedIn/SuccessLogin");
                            }
                        }
                        else if (SessionCheck[1] == SessionID)
                        {
                                    Msg = "Multilple login on the same browser is not allowed";
                                    Console.WriteLine(Msg);
                                    return Page();
                        }
                        else // if user does not have any session
                        {

                            HttpContext.Session.SetString("username", UserName);
                            HttpContext.Session.SetString("email", Customer.EmailAdd);
                            HttpContext.Session.SetString("sessionID", SessionID);
                            Console.WriteLine("This is executed");
                            //saving the session to Db
                            var selectCmd3 = connection.CreateCommand();
                            selectCmd3.CommandText = @"INSERT INTO UserSession (Username, SessionID, DateTime) VALUES ($username, $sessionID, $dateTime)";
                            selectCmd3.Parameters.AddWithValue("$username", UserName);
                            selectCmd3.Parameters.AddWithValue("$sessionID", SessionID);
                            selectCmd3.Parameters.AddWithValue("$dateTime", dateTime);

                            selectCmd3.Prepare();
                            selectCmd3.ExecuteNonQuery();

                            //UserName = HttpContext.Session.Id; //set the variable as session ID to allow multiple session in 1 browser
                            //Customer.EmailAdd = HttpContext.Session.Id;

                            return RedirectToPage("/CustomerLoggedIn/SuccessLogin");
                        }  
                    }
                    else
                    {
                        Msg = "Incorrect ID and PWD!";
                        return Page();
                    }
                
        }


        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove(UserName);
            return Page();
        }



    }

   
}
