﻿using Ivy.Auth0.Core.Models.Interfaces;

namespace Ivy.Auth0.Core.Models.Requests
{
    /*
     * The only values you're going to need to populate here are going to be 
     * email and password.  If you wish to set up some of the alternative values,
     * you definitely can; we'll eventually be setting up roles with this functionality
     * as well.
     */

    public class Auth0CreateUserRequest : IAuth0Phone, IAuth0Username
    {
        #region Constructor

        public Auth0CreateUserRequest()
        {
            verify_email = true;

            // We get errors if these are left null
            user_metadata = new Auth0UserMetadata();
            app_metadata = new Auth0AppMetadata();
        }

        #endregion

        #region Public Attributes

        public string connection { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; set; } // Do not include in JSON if not setup in configs
        public string phone_number { get; set; } // Do not include in JSON if null or empty
        public bool email_verified { get; set; }
        public bool verify_email { get; set; }
        public bool phone_verified { get; set; } // false if no phone number

        public Auth0UserMetadata user_metadata { get; set; }
        public Auth0AppMetadata app_metadata { get; set; }

        #endregion
    }
}