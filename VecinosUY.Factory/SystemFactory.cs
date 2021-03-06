﻿using System;
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
        private static BuildingValidator buildingValidator;
        private static AccountStateValidator accountStateValidator;
        private static ServiceValidator serviceValidator;
        private static BookingValidator bookingValidator;
        private static AnnouncementValidator announcementValidator;
        private static PropertiesValidator propertiesValidator;
        private static MeetingValidator meetingValidator;
        private static VoteValidator voteValidator;
        private static ContactValidator contactValidator;
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
        public static BuildingValidator GetBuildingValidatorInstance()
        {
            if (buildingValidator == null)
            {
                buildingValidator = new BuildingValidator(GetUnitOfWofkInstance());
            }
            return buildingValidator;
        }

        public static VoteValidator GetVoteValidatorInstance()
        {
            if (voteValidator == null)
            {
                voteValidator = new VoteValidator(GetUnitOfWofkInstance());
            }
            return voteValidator;
        }

        public static ContactValidator GetContactValidatorInstance()
        {
            if (contactValidator == null)
            {
                contactValidator = new ContactValidator(GetUnitOfWofkInstance());
            }
            return contactValidator;
        }


        public static ServiceValidator GetServiceValidatorInstance()
        {
            if (serviceValidator == null)
            {
                serviceValidator = new ServiceValidator(GetUnitOfWofkInstance());
            }
            return serviceValidator;
        }

        public static BookingValidator GetBookingValidatorInstance()
        {
            if (bookingValidator == null)
            {
                bookingValidator = new BookingValidator(GetUnitOfWofkInstance());
            }
            return bookingValidator;
        }
        public static AnnouncementValidator GetAnnouncementValidatorInstance()
        {
            if (announcementValidator == null)
            {
                announcementValidator = new AnnouncementValidator(GetUnitOfWofkInstance());
            }
            return announcementValidator;
        }
        public static AccountStateValidator GetAccountStateValidatorInstance()
        {
            if (accountStateValidator == null)
            {
                accountStateValidator = new AccountStateValidator(GetUnitOfWofkInstance());
            }
            return accountStateValidator;
        }

        public static MeetingValidator GetMeetingValidatorInstance()
        {
            if (meetingValidator == null)
            {
                meetingValidator = new MeetingValidator(GetUnitOfWofkInstance());
            }
            return meetingValidator;
        }
    }
}
