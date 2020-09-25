using System;
using System.Collections.Generic;
using BasicFormsApplication;
using BasicFormsApplication.Models.Forms;
using BasicFormsApplication.Models.User;
using BasicFormsApplication.Repositories;
using BasicFormsApplication.UserContextProvider;
using FakeItEasy;
using Xunit;

namespace BasicFormApplicationTests
{
    public class FormsControllerTests
    {
        private const string LoggedInUserName = "John Doe";
        private const string LoggedInUserEmail = "johndoe@contosa.com";
        private const int LoggedInUserId = 12;
        private const string LoggedInUserIpAddress = "127.0.0.1";
        private const string PostcalCodeForGivenIpAddress = "1234AB";
        private const string HouseNumberForGivenIpAddress = "42";
        private const string ProvinceForGivenIpAddress = "Utrecht";
        private const string StreetForGivenIpAddress = "Homestreet";
        private static DateTime DateOfBirth = new DateTime(2000, 09, 20);


        [Theory]
        [InlineData(AddressForm.FORM_NAME, nameof(AddressForm.PostalCode), PostcalCodeForGivenIpAddress)]
        [InlineData(AddressForm.FORM_NAME, nameof(AddressForm.HouseNumber), HouseNumberForGivenIpAddress)]
        [InlineData(PersonalInformationForm.FORM_NAME, nameof(PersonalInformationForm.Name), LoggedInUserName)]
        public void FormsController_WhenGivenFormNameInGetPreFillData_WithAuthenticatedUser_ReturnsRightValue(string formName, string key, string expected)
        {
            // Arrange
            var formRepository = CreateFormRepository();
            var userContextProvider = CreateUserContextProvider();
            var addressProvider = CreateAddressProvider();

            var formsController = new FormsController(formRepository, userContextProvider, addressProvider);

            // Act
            var prefillData = formsController.GetPrefillData(formName);
            var actual = GetPrefillValueIfAny(prefillData, key);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(AddressForm.FORM_NAME, nameof(AddressForm.PostalCode), null)]
        [InlineData(AddressForm.FORM_NAME, nameof(AddressForm.HouseNumber), null)]
        [InlineData(PersonalInformationForm.FORM_NAME, nameof(PersonalInformationForm.Name), null)]
        public void FormsController_WhenGivenFormNameInGetPreFillData_WithoutAuthenticatedUser_ReturnsRightValue(string formName, string key, string expected)
        {
            // Arrange
            var formRepository = CreateFormRepository();
            var userContextProvider = CreateUserContextProvider(false);
            var addressProvider = CreateAddressProvider();

            var formsController = new FormsController(formRepository, userContextProvider, addressProvider);

            // Act
            var prefillData = formsController.GetPrefillData(formName);
            var actual = GetPrefillValueIfAny(prefillData, key);

            // Assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(nameof(AddressForm.Street), StreetForGivenIpAddress)]
        [InlineData(nameof(AddressForm.Province), ProvinceForGivenIpAddress)]
        [InlineData(nameof(AddressForm.HouseNumber), HouseNumberForGivenIpAddress)]
        [InlineData(nameof(AddressForm.PostalCode), PostcalCodeForGivenIpAddress)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserEmail), LoggedInUserEmail)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserId), LoggedInUserId)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserIpAddress), LoggedInUserIpAddress)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserName), LoggedInUserName)]
        public void FormsController_WhenSubmittingAddressForm_WithAuthenticatedUser_SetsRightValue(string key, object expected)
        {
            // Arrange
            var form = CreateAddressForm();
            form.PostalCode = PostcalCodeForGivenIpAddress;
            form.HouseNumber = HouseNumberForGivenIpAddress;
            var formRepository = CreateFormRepository(addressForm: form);
            var userContextProvider = CreateUserContextProvider();
            var addressProvider = CreateAddressProvider();

            var formsController = new FormsController(formRepository, userContextProvider, addressProvider);

            // Act
            formsController.SubmitForm(form);
            var actualValue = form.SubmittedValues[key];

            // Assert
            Assert.Equal(expected, actualValue);
        }
        [Theory]
        [InlineData(nameof(AddressForm.Street), StreetForGivenIpAddress)]
        [InlineData(nameof(AddressForm.Province), ProvinceForGivenIpAddress)]
        [InlineData(nameof(AddressForm.HouseNumber), HouseNumberForGivenIpAddress)]
        [InlineData(nameof(AddressForm.PostalCode), PostcalCodeForGivenIpAddress)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserEmail), null)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserId), null)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserIpAddress), null)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserName), null)]
        public void FormsController_WhenSubmittingAddressForm_WithoutAuthenticatedUser_SetsRightValue(string key, object expected)
        {
            // Arrange
            var form = CreateAddressForm();
            form.PostalCode = PostcalCodeForGivenIpAddress;
            form.HouseNumber = HouseNumberForGivenIpAddress;
            var formRepository = CreateFormRepository(addressForm: form);
            var userContextProvider = CreateUserContextProvider(false);
            var addressProvider = CreateAddressProvider();

            var formsController = new FormsController(formRepository, userContextProvider, addressProvider);

            // Act
            formsController.SubmitForm(form);
            var actualValue = form.SubmittedValues[key];

            // Assert
            Assert.Equal(expected, actualValue);
        }

        [Theory]
        [InlineData(nameof(PersonalInformationForm.Name), "Foo")]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserEmail), LoggedInUserEmail)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserId), LoggedInUserId)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserIpAddress), LoggedInUserIpAddress)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserName), LoggedInUserName)]
        public void FormsController_WhenSubmittingPersonalForm_WithAuthenticatedUser_SetsRightValue(string key, object expected)
        {
            // Arrange
            var form = CreatePersonalInformationForm();
            form.Name = "Foo";
            form.DateOfBirth = DateOfBirth;
            var formRepository = CreateFormRepository(personalInformationForm: form);
            var userContextProvider = CreateUserContextProvider();
            var addressProvider = CreateAddressProvider();

            var formsController = new FormsController(formRepository, userContextProvider, addressProvider);

            // Act
            formsController.SubmitForm(form);
            var actualValue = form.SubmittedValues[key];

            // Assert
            Assert.Equal(expected, actualValue);
        }

        [Theory]
        [InlineData(nameof(PersonalInformationForm.Name), "Foo")]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserEmail), null)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserId), null)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserIpAddress), null)]
        [InlineData(nameof(IAuditInformation.AuthenticatedUserName), null)]
        public void FormsController_WhenSubmittingPersonalForm_WithoutAuthenticatedUser_SetsRightValue(string key, object expected)
        {
            // Arrange
            var form = CreatePersonalInformationForm();
            form.Name = "Foo";
            form.DateOfBirth = DateOfBirth;
            var formRepository = CreateFormRepository(personalInformationForm: form);
            var userContextProvider = CreateUserContextProvider(false);
            var addressProvider = CreateAddressProvider();

            var formsController = new FormsController(formRepository, userContextProvider, addressProvider);

            // Act
            formsController.SubmitForm(form);
            var actualValue = form.SubmittedValues[key];

            // Assert
            Assert.Equal(expected, actualValue);
        }

        private IUserContextProvider CreateUserContextProvider(bool shouldReturnAuthenticatedUser = true)
        {
            var fake = A.Fake<IUserContextProvider>();
            if (shouldReturnAuthenticatedUser)
            {
                A.CallTo(() => fake.GetCurrentAuthenticatedUser()).Returns(new User
                {
                    Email = LoggedInUserEmail,
                    Id = LoggedInUserId,
                    IpAddress = LoggedInUserIpAddress,
                    Name = LoggedInUserName
                });
            }
            else
            {
                A.CallTo(() => fake.GetCurrentAuthenticatedUser()).Returns(null);
            }

            return fake;
        }
        private IFormRepository CreateFormRepository(AddressForm addressForm = null, PersonalInformationForm personalInformationForm = null)
        {
            if(addressForm == null)
            {
                addressForm = new AddressForm();
            }
            if (personalInformationForm == null)
            {
                personalInformationForm = new PersonalInformationForm();
            }

            var fake = A.Fake<IFormRepository>();
            A.CallTo(() => fake.GetFormDefinition(AddressForm.FORM_NAME)).Returns(addressForm);
            A.CallTo(() => fake.GetFormDefinition(PersonalInformationForm.FORM_NAME)).Returns(personalInformationForm);
            return fake;
        }

        private AddressForm CreateAddressForm()
        {
            return new AddressForm();
        }

        private PersonalInformationForm CreatePersonalInformationForm()
        {
            return new PersonalInformationForm();
        }

        private IAddressProvider CreateAddressProvider()
        {
            var fake = A.Fake<IAddressProvider>();
            A.CallTo(() => fake.GetPostalCodeAndHouseNumber(LoggedInUserIpAddress)).Returns((PostcalCodeForGivenIpAddress, HouseNumberForGivenIpAddress));
            A.CallTo(() => fake.GetProvinceAndStreet(PostcalCodeForGivenIpAddress, HouseNumberForGivenIpAddress)).Returns((ProvinceForGivenIpAddress, StreetForGivenIpAddress));

            return fake;
        }

        private object GetPrefillValueIfAny(Dictionary<string, object> preFillValues, string key)
        {
            if(!preFillValues.ContainsKey(key))
            {
                return null;
            }

            return preFillValues[key];
        }
    }
}
