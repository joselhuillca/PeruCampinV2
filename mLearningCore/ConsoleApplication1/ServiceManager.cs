using Core.Repositories;
using MLearning.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ServiceManager
    {

        static IMLearningService mlearningService = null;
        static IRepositoryService repositoryService = null;

        public static IMLearningService GetService()
        {
            if (repositoryService == null)
            {
                repositoryService = new WAMSRepositoryService();
            }

            if (mlearningService == null)
            {
                mlearningService = new MLearningAzureService(repositoryService);
            }

            return mlearningService;
        }
    }
}
