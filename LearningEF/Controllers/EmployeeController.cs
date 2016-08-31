using LearningEF.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LearningEF.Controllers
{
    public class EmployeeController : ApiController
    {
        public IHttpActionResult Get()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);

            using (var context = new mydbEntities())
            {
                var Query = context.Employees.ToList<Employee>();
                string dataString = "";
                foreach (var person in Query)
                {
                    dataString += person.Firstname + " " + person.Lastname + " " + person.Position + " " + person.Salary + "<br>";
                }
                response.Content = new StringContent("<htmk> Fetch ,, data have -> <br>" + dataString + "</html>");
                return ResponseMessage(response);
            }
        }

        public IHttpActionResult Get(int id)
        {
            using (var context = new mydbEntities())
            {
                var Query = context.Employees.Where(employee => employee.id == id);
                var response = new HttpResponseMessage();

                if (Query.Count() > 0)
                {
                    var employee_found = Query.FirstOrDefault<Employee>();
                    string dataString = employee_found.Firstname + " " + employee_found.Lastname + " " + employee_found.Position + " " + employee_found.Salary + "<br>";
                    response.Content = new StringContent("<htmk> Fetch ,, data have -> <br>" + dataString + "</html>");
                    return ResponseMessage(response);
                }
                else
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Content = new StringContent("<html> Can't find your employee</html>");
                    return ResponseMessage(response);
                }
            }
        }

        /*
         * Example method for receive Data from request
         */

        /*
        public IHttpActionResult Post(MyDataForm formData)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            if (formData.Data != null)
            {
                response.Content = new StringContent(formData.Data);
            }
            else
            {
                response.Content = new StringContent("NULL");
            }

            return ResponseMessage(response);
        }
        */
        

        /*
        public IHttpActionResult Post(Employee employee)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            string responseString = employee.Firstname + " " + employee.Lastname + " " + employee.Position + " " + employee.Salary;
            response.Content = new StringContent(responseString);
            return ResponseMessage(response);
        }
        */

    }
}
