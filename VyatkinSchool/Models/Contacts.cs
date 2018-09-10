using MixERP.Net.VCards;
using MixERP.Net.VCards.Models;
using MixERP.Net.VCards.Serializer;
using MixERP.Net.VCards.Types;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace VyatkinSchool.Models
{
    public class Contacts
    {
        public Contacts()
        {
            InitDefaultValue();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string FullName { get { return $"{LastName} {FirstName} {Patronymic}"; } }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Skype { get; set; }
        public Uri WebSite { get; set; }
        public string Location { get; set; }
        public byte[] QrCode { get { return GetQrCodeBytes(); } }

        public MemoryStream GetVCardStream()
        {
            var vrCard = GetVcard();
            return new MemoryStream(ASCIIEncoding.Default.GetBytes(vrCard));
        }

        private string GetVcard()
        {
            var vcard = new VCard
            {
                Version = VCardVersion.V4,
                FormattedName = $"{FirstName} {LastName}",
                FirstName = FirstName,
                LastName = LastName,
                Classification = ClassificationType.Public,
                Categories = new[] { "Тренер по вольной борьбе" },
                Emails = new List<Email>() { new Email() { EmailAddress = Email.ToLower() } },
                Telephones = new List<Telephone>() { new Telephone() { Number = Phone, Type = TelephoneType.Cell } },
                Url = WebSite,
                DeliveryAddress = new DeliveryAddress() { Address = Location }
            };
            return vcard.Serialize();
        }

        private byte[] GetQrCodeBytes()
        {
            var qrGenerator = new QRCodeGenerator();
            var vrCard = GetVcard();
            var qrCodeData = qrGenerator.CreateQrCode(vrCard, QRCodeGenerator.ECCLevel.M);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeBitMap = qrCode.GetGraphic(pixelsPerModule: 2);
            return (byte[])new ImageConverter().ConvertTo(qrCodeBitMap, typeof(byte[]));
        }

        private void InitDefaultValue()
        {
            FirstName = "Олег";
            LastName = "Вяткин";
            Patronymic = "Олегович";
            Phone = "+7(978)118-51-82";
            Email = "oleg@sevwrestling.ru";
            WebSite = new Uri("https://www.sevwrestling.ru");
            Location = "Россия, г. Севастополь";
        }
    }
}