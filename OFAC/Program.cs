using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;

namespace OFAC
{
    class Program
    {
        static void Main(string[] args)
        {            
            // **** setup automapper ****
            Mapper.CreateMap<sdnList, sdnListM>();
            Mapper.CreateMap<sdnListPublshInformation, sdnListPublshInformationM>();
            Mapper.CreateMap<sdnListSdnEntry, sdnListSdnEntryM>()
                .ForMember(d => d.programList, opt => opt.MapFrom(src => String.Join(";", src.programList)));
            Mapper.CreateMap<sdnListSdnEntryID, sdnListSdnEntryIDM>();
            Mapper.CreateMap<sdnListSdnEntryAka, sdnListSdnEntryAkaM>();
            Mapper.CreateMap<sdnListSdnEntryAddress, sdnListSdnEntryAddressM>();
            Mapper.CreateMap<sdnListSdnEntryNationality, sdnListSdnEntryNationalityM>();
            Mapper.CreateMap<sdnListSdnEntryCitizenship, sdnListSdnEntryCitizenshipM>();
            Mapper.CreateMap<sdnListSdnEntryDateOfBirthItem, sdnListSdnEntryDateOfBirthItemM>();
            Mapper.CreateMap<sdnListSdnEntryPlaceOfBirthItem, sdnListSdnEntryPlaceOfBirthItemM>();
            Mapper.CreateMap<sdnListSdnEntryVesselInfo, sdnListSdnEntryVesselInfoM>();

            // **** get latest data from http://www.treasury.gov/ofac/downloads/sdn.xml ****
            Console.WriteLine("Downloading data from http://www.treasury.gov/ofac/downloads/sdn.xml");
            var data = GetData();

            // **** convert data to entity framework models ****
            sdnListM sdnListM = Mapper.Map<sdnListM>(data);

            // **** setup db and insert data ****
            Console.WriteLine("Inserting data to db.. Please wait");
            Database.SetInitializer<OfacContext>(new CreateDatabaseIfNotExists<OfacContext>());
            OfacContext ofacContext = new OfacContext();
            ofacContext.sdnList.Add(sdnListM);
            ofacContext.SaveChanges();

            Console.WriteLine("Finished.");
            Console.ReadLine();
        }

        public static sdnList GetData()
        {
            WebClient client = new WebClient();
            string res = client.DownloadString("http://www.treasury.gov/ofac/downloads/sdn.xml");
            File.WriteAllText(@"..\..\sdn.xml", res);

            sdnList sr = new sdnList();

            using (FileStream xmlStream = new FileStream(@"..\..\sdn.xml", FileMode.Open))
            {
                using (XmlReader xmlReader = XmlReader.Create(xmlStream))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(sdnList));
                    sdnList deserialized = serializer.Deserialize(xmlReader) as sdnList;
                    return deserialized;
                }
            }
        }
    }
}
