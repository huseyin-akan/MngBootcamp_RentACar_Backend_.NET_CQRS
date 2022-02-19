using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Messages
{
    public static class Messages
    {
        public static string CarAdded = "Car has been added successfully";
        public static string CarNotFound = "Car with that id can not be found!!";
        public static string CarUpdateFailed= "Car can not be updated. Something is wrong!!";
        public static string KilometerCantBeLess = "When ending rental car kiloemeter can not be less than the kilometer when it was rented!!";

        public static string ModelNotFound = "Model can not be found!!!";
        public static string PaymentFailed = "Payment is not successful!!";

        public static string ColorNotFound = "Color with that id can not be found!!";

        public static string InvoiceNotAdded = "Invoice can not be created. Something is wrong!!";

        public static string BrandNameExists = "Brand name already exists";
        public static string BrandNotFound = "Brand doesnt exist.";

        public static string UserNotFound = "User is not found";
        public static string PasswordError = "Password is wrong. Try again!!";

        public static string IndividualCustomerDoesntExist = "No Individual customer with that id exists!!";
        public static string CorporateCustomerDoesntExist = "No Corporate customer with that id exists!!";
        public static string FindexScoreNotEnough = "Findex score is not enough to rent the car!!";
        public static string NationalIdAlreadyUsed = "This national id was used before!!";
        public static string TaxNumberAlreadyUsed = "This tax number was used before!!";
        public static string UsernameAlreadyTaken = "This username was taken already. Please choose another username!!";
        public static string EmailAlreadyTaken = "This email was taken already. If you forgot your password, try to reset it";

        public static string TotalSumCalculationIsWrong = "The demanded total sum doesnt match the calculated total sum";

    }
}
