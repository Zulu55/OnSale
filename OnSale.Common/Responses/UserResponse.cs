using System;
using OnSale.Common.Entities;
using OnSale.Common.Enums;

namespace OnSale.Common.Responses
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public Guid ImageId { get; set; }

        public string ImageFacebook { get; set; }

        public LoginType LoginType { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (LoginType == LoginType.Facebook && string.IsNullOrEmpty(ImageFacebook) ||
                    LoginType == LoginType.OnSale && ImageId == Guid.Empty)
                {
                    return $"https://onsaleprepweb.azurewebsites.net/images/noimage.png";
                }

                if (LoginType == LoginType.Facebook)
                {
                    return ImageFacebook;
                }

                return $"https://onsale.blob.core.windows.net/users/{ImageId}";
            }
        }

        public UserType UserType { get; set; }

        public City City { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public double Latitude { get; set; }

        public double Logitude { get; set; }
    }
}
