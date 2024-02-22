using SPCaemucals.Backend.Controllers;
using SPCaemucals.Backend.Dto;
using SPCaemucals.Backend.Dto.Model;
using SPCaemucals.Data.Enum;
using SPCaemucals.Data.Identities;
using SPCaemucals.Data.Models;

namespace SPChemicalTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(
            "168/184 ตำบลบ้านกรด", 1, 1, 1, 1, // Parameters for Address object
            "168/184 ตำบลบ้านกรด", 1, 1, 1, 1 // Parameters for AddressDTO object
        )]
        [InlineData(
            "168/184 ตำบลบ้านกรด   ", 1, 1, 1, 1,
            "168/184 ตำบลบ้านกรด", 1, 1, 1, 1
        )]
        public void Given_AddNewAddress_When_ComparedWithAddressDTO_Then_ReturnTrue(
            string addressDetail, int addressProvinceId, int addressDistrictId, int addressSubDistrictId,
            int addressPostalCodeId,
            string dtoDetail, int dtoProvinceId, int dtoDistrictId, int dtoSubDistrictId, int dtoPostalCodeId)
        {
            // Arrange
            Address address = new Address()
            {
                AddressDetail = addressDetail,
                ProvinceId = addressProvinceId,
                DistrictId = addressDistrictId,
                SubDistrictId = addressSubDistrictId,
                PostalCodeCodeId = addressPostalCodeId,
            };
            AddressDTO addressDto = new AddressDTO()
            {
                AddressDetail = dtoDetail,
                ProvinceId = dtoProvinceId,
                DistrictId = dtoDistrictId,
                SubDistrictId = dtoSubDistrictId,
                PostalCodeCodeId = dtoPostalCodeId,
            };

            // Act
            bool result = addressDto.Equals(address);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Given_Receive_When_AssignValue_then_ReturnCustomer()
        {
            //set
            ReceiverDetails senderDetails = new ReceiverDetails()
            {
                PhoneNo = "0918131501",
                SaveAddress = true,
                Firstname = "ทศพล",
                Lastname = "sonthiphin",
                Province = new ProvinceDTO() { Id = 1 },
                District = new DistrictDTO()
                {
                    Id = 1
                },
                SubDistrict = new SubDistrictDTO() { Id = 1 },
                PostalCode = new PostalDTO() { Id = 1 },
                AddressText = "168/184",
                Cod = true
            };

            Customer customer = senderDetails.GetCustomer();
            
            Assert.Equal(senderDetails.PhoneNo,customer.PhoneNo);
            Assert.Equal(senderDetails.Firstname,customer.FirstName);
            Assert.Equal(senderDetails.Lastname,customer.LastName);
            Assert.Equal(CustomerType.Temporary,customer.CustomerType);



        }
        
        [Fact]
        public void Given_Receive_When_AssignValue_then_ReturnAddress()
        {
            //set
            ReceiverDetails senderDetails = new ReceiverDetails()
            {
                PhoneNo = "0918131501",
                SaveAddress = true,
                Firstname = "ทศพล",
                Lastname = "sonthiphin",
                Province = new ProvinceDTO() { Id = 1 },
                District = new DistrictDTO()
                {
                    Id = 2
                },
                SubDistrict = new SubDistrictDTO() { Id = 3 },
                PostalCode = new PostalDTO() { Id = 4 },
                AddressText = "168/184",
                Cod = true
            };

            Address address = senderDetails.GetAddress();
            Assert.Equal(senderDetails.AddressText,address.AddressDetail);
            Assert.Equal(senderDetails.Province.Id,address.ProvinceId);
            Assert.Equal(senderDetails.District.Id,address.DistrictId);
            Assert.Equal(senderDetails.SubDistrict.Id,address.SubDistrictId);
            Assert.Equal(senderDetails.PostalCode.Id,address.PostalCodeCodeId);



        }
    }
}