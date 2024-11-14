using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalService.Classes
{
    public class Events
    {
        public string EventName { get; set; }
        public string EventCategory { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public string EventDescription { get; set; }
        public string ImagePath { get; set; } // New property for the event image

        // Sample data with event images
        public static List<Events> GetSampleEvents()
        {
            return new List<Events>
                    {
                        new Events {
                            EventName = "Town Hall Meeting",
                            EventCategory = "Government",
                            EventDate = DateTime.Now.AddDays(3),
                            EventLocation = "Main Hall",
                            EventDescription = "Join us for a town hall meeting where we will discuss the municipal budget and its impact on our community. This is a great opportunity to engage with local officials and have your voice heard. Refreshments will be provided.",
                            ImagePath="Images/townhall.jpg"
                        },
                        new Events {
                            EventName = "Music Festival",
                            EventCategory = "Festival",
                            EventDate = DateTime.Now.AddDays(10),
                            EventLocation = "City Park",
                            EventDescription = "Get ready to groove at our annual music festival! This year's lineup features a diverse range of local artists and bands, from rock to jazz to folk. Enjoy delicious food and drinks, browse our vendor market, and soak up the lively atmosphere. Admission is free, so bring the whole family!",
                            ImagePath="Images/musicfestival.jpg"
                        },
                        new Events {
                            EventName = "Water Conservation Workshop",
                            EventCategory = "Workshop",
                            EventDate = DateTime.Now.AddDays(7),
                            EventLocation = "Library",
                            EventDescription = "Learn how to save water and reduce your environmental impact at our water conservation workshop. Our expert instructors will share tips and techniques for conserving water in your home and garden, and provide resources for further learning. Registration is required, so sign up today!",
                            ImagePath="Images/workshop.jpg"
                        },
                        new Events {
                            EventName = "Sports Day",
                            EventCategory = "Sports",
                            EventDate = DateTime.Now.AddDays(14),
                            EventLocation = "Sports Complex",
                            EventDescription = "Get ready for a fun-filled day of sports and activities at our annual Sports Day! Join us for a morning of games, competitions, and demonstrations, followed by a picnic lunch and awards ceremony. All ages and abilities are welcome, so come out and show your spirit!",
                            ImagePath="Images/sportsday.jpg"
                        },
                        new Events {
                             EventName = "Community Yard Sale",
                             EventCategory = "Community",
                            EventDate = DateTime.Now.AddDays(5),
                            EventLocation = "Community Center",
                            EventDescription = "Come and find some great deals at our community yard sale! Browse through a variety of gently used items, from household goods to clothing and more. All proceeds will go towards supporting local community programs.",
                            ImagePath="Images/communityyardsale.jpg"
        }
                    };
        }

        // Method to get recommended events based on category
        public static List<Events> GetRecommendedEventsByCategory(string category)
        {
            var recommendedEvents = new List<Events>();

            switch (category.ToLower())
            {
                case "government":
                    recommendedEvents.Add(new Events
                    {
                        EventName = "City Council Meeting",
                        EventCategory = "Government",
                        EventDate = DateTime.Now.AddDays(5),
                        EventLocation = "City Hall",
                        EventDescription = "Attend a city council meeting to stay informed about local government decisions and initiatives. This is a great opportunity to engage with your elected officials and have your voice heard. Meetings are open to the public and typically last about an hour.",
                        ImagePath = "Images/citycouncil.jpg"
                    });
                    //still need a pic
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Public Safety Forum",
                        EventCategory = "Government",
                        EventDate = DateTime.Now.AddDays(12),
                        EventLocation = "Community Center",
                        EventDescription = "Join us for a public safety forum where we will discuss crime prevention strategies and community policing initiatives. Our expert panel will include local law enforcement officials and community leaders. Refreshments will be provided.",
                        ImagePath = "Images/publicsafety.jpg"
                    });
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Budget Planning Session",
                        EventCategory = "Government",
                        EventDate = DateTime.Now.AddDays(20),
                        EventLocation = "Main Hall",
                        EventDescription = "Participate in our budget planning session to help shape the future of our community. We will discuss budget priorities and provide an overview of the municipal budget process. Your input is valuable, so come out and make your voice heard!",
                        ImagePath = "Images/budgetplanning.jpg"
                    });
                    break;
                case "festival":
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Food Festival",
                        EventCategory = "Festival",
                        EventDate = DateTime.Now.AddDays(8),
                        EventLocation = "Downtown",
                        EventDescription = "Indulge in a culinary extravaganza at our food festival! Sample dishes from local restaurants and food trucks, and browse our vendor market featuring artisanal goods and specialty foods. Admission is free, so bring the whole family!",
                        ImagePath = "Images/foodfestival.jpg"
                    });
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Art Festival",
                        EventCategory = "Festival",
                        EventDate = DateTime.Now.AddDays(15),
                        EventLocation = "Art District",
                        EventDescription = "Celebrate the arts at our annual art festival! This year's festival features a diverse range of local artists, from painters to sculptors to photographers. Enjoy live music, food, and drinks, and take part in interactive art activities. Admission is free, so come out and support local talent!",
                        ImagePath = "Images/artfestival.jpg"
                    });
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Film Festival",
                        EventCategory = "Festival",
                        EventDate = DateTime.Now.AddDays(22),
                        EventLocation = "Cinema Hall",
                        EventDescription = "Experience the magic of cinema at our film festival! This year's lineup features a curated selection of independent films, documentaries, and shorts. Meet the filmmakers, attend workshops, and enjoy the festive atmosphere. Tickets are available online or at the door.",
                        ImagePath = "Images/filmfestival.jpg"
                    });
                    break;
                case "workshop":
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Gardening Workshop",
                        EventCategory = "Workshop",
                        EventDate = DateTime.Now.AddDays(6),
                        EventLocation = "Community Garden",
                        EventDescription = "Get your hands dirty at our gardening workshop! Learn about sustainable gardening practices, composting, and urban agriculture from our expert instructors. Take home some new skills and a packet of seeds to get started on your own gardening journey. Registration is required, so sign up today!",
                        ImagePath = "Images/gardeningworkshop.jpg"
                    });
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Tech Workshop",
                        EventCategory = "Workshop",
                        EventDate = DateTime.Now.AddDays(13),
                        EventLocation = "Tech Hub",
                        EventDescription = "Unlock your tech potential at our tech workshop! Learn about coding, robotics, and digital media from our expert instructors. Take part in hands-on activities and projects, and network with like-minded individuals. Registration is required, so sign up today!",
                        ImagePath = "Images/techworkshop.jpg"
                    });
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Cooking Workshop",
                        EventCategory = "Workshop",
                        EventDate = DateTime.Now.AddDays(19),
                        EventLocation = "Culinary School",
                        EventDescription = "Savor the flavors at our cooking workshop! Learn about international cuisine, cooking techniques, and meal planning from our expert chefs. Take home some new recipes and cooking confidence. Registration is required, so sign up today!",
                        ImagePath = "Images/cookingworkshop.jpg"
                    });
                    break;
                case "sports":
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Basketball Tournament",
                        EventCategory = "Sports",
                        EventDate = DateTime.Now.AddDays(9),
                        EventLocation = "Sports Complex",
                        EventDescription = "Get ready for a slam dunk at our basketball tournament! Join us for a fun-filled day of competition, sportsmanship, and community spirit. Registration is required for teams, but spectators are welcome to attend and cheer on their favorite teams.",
                        ImagePath = "Images/basketballtournament.jpg"
                    });
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Marathon",
                        EventCategory = "Sports",
                        EventDate = DateTime.Now.AddDays(16),
                        EventLocation = "City Streets",
                        EventDescription = "Lace up your running shoes for our annual marathon! This year's course takes you through the scenic city streets, with water stations and cheering crowds along the way. Register online or in person, and receive a finisher's medal and t-shirt.",
                        ImagePath = "Images/marathon.jpg"
                    });
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Swimming Competition",
                        EventCategory = "Sports",
                        EventDate = DateTime.Now.AddDays(23),
                        EventLocation = "Aquatic Center",
                        EventDescription = "Dive into the action at our swimming competition! Join us for a morning of racing, relays, and fun in the pool. Registration is required for competitors, but spectators are welcome to attend and cheer on their favorite swimmers.",
                        ImagePath = "Images/swimmingcompetition.jpg"
                    });
                    break;
                case "community":
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Neighborhood Clean-up",
                        EventCategory = "Community",
                        EventDate = DateTime.Now.AddDays(4),
                        EventLocation = "Local Park",
                        EventDescription = "Join us for a neighborhood clean-up event! Help keep our community clean and beautiful. Gloves and trash bags will be provided.",
                        ImagePath = "Images/neighborhoodcleanup.jpg"
                    });
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Volunteer Fair",
                        EventCategory = "Community",
                        EventDate = DateTime.Now.AddDays(11),
                        EventLocation = "Community Center",
                        EventDescription = "Find your passion and make a difference at our volunteer fair! Meet with local organizations and learn about volunteer opportunities in our community.",
                        ImagePath = "Images/volunteerfair.jpg"
                    });
                    recommendedEvents.Add(new Events
                    {
                        EventName = "Community Potluck",
                        EventCategory = "Community",
                        EventDate = DateTime.Now.AddDays(18),
                        EventLocation = "Community Hall",
                        EventDescription = "Bring a dish to share and join us for a community potluck! Meet your neighbors and enjoy some great food and company.",
                        ImagePath = "Images/communitypotluck.jpg"
                    });
                    break;
                default:
                    break;
            }

            return recommendedEvents;
        }

        public override string ToString()
        {
            return $"{EventName} - {EventCategory} - {EventDate.ToShortDateString()}";
        }
    }
}