using Bogus;
using System;

namespace ProjectBDDRegsitrationProcess.Environment
{
    public class Testdatagenerator
    {
        public string baseurl = null;
        public string baseendpointurl = null;
        public string DevuserSignInEmail = null;
        public string DevuserSignInPassword = null;
        public string RandFirstName = null;
        public string RandLastName = null;
        Faker faker = new Faker();
        Random rnd = new Random();

        public string GenEmailaddress()
        {
            string DevuserSignInEmail = "testreg"+rnd.Next(2000, 8000)+"@autopractice.com";
            return DevuserSignInEmail;
        }

        public string Genpostcode()
        {

            string ZipCode = "50"+rnd.Next(300, 800);
            return ZipCode;
        }

        public string GenFirstName()
        {

            RandFirstName = faker.Name.FirstName();
            return RandFirstName;
        }

        public string GenLastName()
        {

            RandLastName = faker.Name.LastName();
            return RandLastName;
        }

        public string GenAddressLine1()
        {

            var RandAddressLine1 = faker.Address.StreetName();
            return RandAddressLine1;
        }

        public string GenAddressLine2()
        {

            var RandAddressLine2 = faker.Address.StreetAddress();
            return RandAddressLine2;
        }

        public string GenCity()
        {

            var RandCity = faker.Address.City();
            return RandCity;
        }

        public string GenMobilenumber()
        {
            string RandMobNumber = "09100" + rnd.Next(70000, 90000);
            return RandMobNumber;
        }
    }
}
