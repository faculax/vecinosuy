using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VecinosUY.Data.Repository;
using VecinosUY.Logic;
using VecinosUY.Data.DataAccess;
using VecinosUY.Logger;
using VecinosUY.PlainTextLogger;

namespace VecinosUY.Factory
{
    public class SystemFactory
    {
        private static UnitOfWork unitOfWork;
        private static UserValidator userValidator;
        private static AnnouncementValidator announcementValidator;
        private static PropertiesValidator propertiesValidator;
        private static ILogger logger;

        public static UnitOfWork GetUnitOfWofkInstance()
        {
            if (unitOfWork == null)
            {
                unitOfWork = new UnitOfWork(new VecinosUYContext());
            }
            return unitOfWork;
        }

        public static ILogger GetLogger()
        {
            if (logger == null)
            {
                logger = new PlainTextLog();
            }
            return logger;
        }

        public static PropertiesValidator GetPropertiesValidatorInstance()
        {
            if (propertiesValidator == null)
            {
                propertiesValidator = new PropertiesValidator(GetUnitOfWofkInstance());
            }
            return propertiesValidator;
        }
        public static UserValidator GetUserValidatorInstance()
        {
            if (userValidator == null)
            {
                userValidator = new UserValidator(GetUnitOfWofkInstance());
            }
            return userValidator;
        }

        public static AnnouncementValidator GetAnnouncementValidatorInstance()
        {
            if (announcementValidator == null)
            {
                announcementValidator = new AnnouncementValidator(GetUnitOfWofkInstance());
            }
            return announcementValidator;
        }
    }
}
