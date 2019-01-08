using System;
using System.Collections.Generic;
using System.Text;

namespace BOS.Activity.Client.ClientModels
{
    public class ActivityError
    {
        public int ErrorCode { get; set; }
        public string Message { get; set; }

        public ActivityError(int errorCode)
        {
            ErrorCode = this.DetermineBOSCode(errorCode);
            Message = this.DetermineMessage(errorCode);
        }

        private int DetermineBOSCode(int errorCode)
        {
            switch (errorCode)
            {
                case 400:
                    return 101;
                case 409:
                    return 409;
                default:
                    return -1;
            }
        }

        private string DetermineMessage(int errorCode)
        {
            switch (errorCode)
            {
                case 400:
                    return "The model structure that you provided is not valid.";
                case 409:
                    return "Acivity with that Id already exists.";
                default:
                    return "";
            }
        }
    }
}
