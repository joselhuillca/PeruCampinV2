
using Core.ViewModels;
using MLearning.Core.Services;
using MLearningDBResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {


            Login();

            Console.ReadLine();
        }


       static async public void Login()
        {
            Console.WriteLine("Hasta aca llegue");
            IMLearningService _mLearningService = ServiceManager.GetService();
            //var com = new LoginViewModel(_mLearningService);
            //com.Name = "aocsa";
            Console.WriteLine("Hasta aca llegue222");
            //com.Password = "1";
            string username = "aocsa";

            string password = "1";

            User user = new User { username = username, password = password };

            var result = await _mLearningService.ValidateLogin<User>(user, u => u.password == user.password && u.username == user.username, u => u.id, u => u.type);

            Console.WriteLine("ACCEDIENDO A LOS SERVICIOS = " + result.successful);
            
        }
    }
}
