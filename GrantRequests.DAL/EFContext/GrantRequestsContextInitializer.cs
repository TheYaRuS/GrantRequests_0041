using GrantRequests.DAL.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using GrantRequests.Common;
using System;

namespace GrantRequests.DAL.EFContext
{
    public class GrantRequestsContextInitializer : /*DropCreateDatabaseAlways<GrantRequestsContext>*/
                                                     CreateDatabaseIfNotExists<GrantRequestsContext>
    {
        protected override void Seed(GrantRequestsContext context)
        {
            var listOfUser = new List<User>
            {
                new User
                {
                    Name = "Admin",
                    Password = "1",
                    Role = Role.Admin
                },
                    new User
                {
                    Name = "PointPerson1",
                    Password = "1",
                    Role = Role.PointPerson
                },
                    new User
                {
                    Name = "PointPerson2",
                    Password = "1",
                    Role = Role.PointPerson                    
                   
                },
                    new User
                {
                    Name = "Requestor1",
                    Password = "1",
                    Role = Role.Requestor
                },
                    new User
                {
                    Name = "Requestor2",
                    Password = "1",
                    Role = Role.Requestor
                },
                    new User
                {
                    Name = "Approver1",
                    Password = "1",
                    Role = Role.Approver
                },
                    new User
                {
                    Name = "Approver2",
                    Password = "1",
                    Role = Role.Approver
                },
                    new User
                {
                    Name = "Approver3",
                    Password = "1",
                    Role = Role.Approver
                }
            };

            var listOfCountry = new List<Country>
            {
                new Country { Title="United States" },
                new Country { Title="European Union" },
                new Country { Title="Japan" },
                new Country { Title="Russia" },
                new Country { Title="China" },
            };

            var listOfState = new List<State>
            {
                new State { Abbreviation = "AL", Title="Alabama" },
                new State { Abbreviation = "AK", Title="Alaska" },
                new State { Abbreviation = "AZ", Title="Arizona" },
                new State { Abbreviation = "AR", Title="Arkansas" },
                new State { Abbreviation = "CA", Title="California" }
            };

            var listOfHealthcareProfession = new List<HealthcareProfession>
            {
                new HealthcareProfession { Title = "HealthcareProfession_1"},
                new HealthcareProfession { Title = "HealthcareProfession_2"},
                new HealthcareProfession { Title = "HealthcareProfession_3"},
                new HealthcareProfession { Title = "HealthcareProfession_4"},
                new HealthcareProfession { Title = "HealthcareProfession_5"}
            };

            var listOfTypeOfDisplayExhibit = new List<TypeOfDisplayExhibit>
            {
                new TypeOfDisplayExhibit{Title = "TypeOfDisplayExhibit_1"},
                new TypeOfDisplayExhibit{Title = "TypeOfDisplayExhibit_2"},
                new TypeOfDisplayExhibit{Title = "TypeOfDisplayExhibit_3"},
                new TypeOfDisplayExhibit{Title = "TypeOfDisplayExhibit_4"},
                new TypeOfDisplayExhibit{Title = "TypeOfDisplayExhibit_5"}
            };

            var listOfBenefit = new List<Benefit>
            {
                new Benefit {Title = "Benefit_1" },
                new Benefit {Title = "Benefit_2" },
                new Benefit {Title = "Benefit_3" },
                new Benefit {Title = "Benefit_4" },
                new Benefit {Title = "Benefit_5" }
            };

            var listOfTherapeuticArea = new List<TherapeuticArea>
            {
                new TherapeuticArea
                {
                    Title = "TherapeuticArea_1",
                    PointPersonal = listOfUser[1],
                    TypeRequest = RequestType.ScientificFunding                    
                },
                new TherapeuticArea
                {
                    Title = "TherapeuticArea_2",
                    PointPersonal = listOfUser[1],
                    TypeRequest = RequestType.PatientAdvocasy
                },
                new TherapeuticArea
                {
                    Title = "TherapeuticArea_3",
                    PointPersonal = listOfUser[1],
                    TypeRequest = RequestType.DisplayAndExhibit
                },
                new TherapeuticArea
                {
                    Title = "TherapeuticArea_4",
                    PointPersonal = listOfUser[2],
                    TypeRequest = RequestType.ScientificFunding
                },
                new TherapeuticArea
                {
                    Title = "TherapeuticArea_5",
                    PointPersonal = listOfUser[2],
                    TypeRequest = RequestType.PatientAdvocasy
                },
                new TherapeuticArea
                {
                    Title = "TherapeuticArea_6",
                    PointPersonal = listOfUser[2],
                    TypeRequest = RequestType.DisplayAndExhibit
                }
            };

            context.Set<Country>().AddRange(listOfCountry);
            context.Set<State>().AddRange(listOfState);
            context.Set<HealthcareProfession>().AddRange(listOfHealthcareProfession);
            context.Set<TypeOfDisplayExhibit>().AddRange(listOfTypeOfDisplayExhibit);
            context.Set<Benefit>().AddRange(listOfBenefit);
            context.Set<User>().AddRange(listOfUser);
            context.Set<TherapeuticArea>().AddRange(listOfTherapeuticArea);
            context.SaveChanges();

        }
    }
}
