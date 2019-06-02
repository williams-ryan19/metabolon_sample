using System;
using System.Collections.Generic;

namespace Metabolon
{
    public class Program
    {
        public Company company;
        public List<RSSFeed> feeds;
        public double maxDays;

        public Program(Tuple<Company, List<RSSFeed>> cfPair, double days)
        {
            //Set each variable, and check to see if any null/faulty parameters were passed
            company = cfPair.Item1;
            if(company == null)
            {
                throw new ArgumentNullException("company can not be null");
            }

            feeds = cfPair.Item2;
            if(feeds == null)
            {
                throw new ArgumentNullException("feeds can not be null");
            }

            maxDays = days;
            if (maxDays < 0)
            {
                throw new ArgumentOutOfRangeException("days passed cannot be negative");
            }

        }

        //main function that determines whether or not the company has updated their feed in x days
        public void isOutdated()
        {
            int recentUpdates = 0;
            foreach (RSSFeed feed in feeds)
            {
                //Calculate how many days have passed since today (the day the feeds are checked)
                DateTime current = DateTime.Today;
                TimeSpan timePassed = current.Subtract(feed.lastUpdate);
                double daysPassed = timePassed.TotalDays;

                if (daysPassed < maxDays)
                {
                    recentUpdates++;
                }

            }
            if (recentUpdates == 0)
            {
                //Writes to the console, displaying the names of the companies that have not updated in x days
                Console.WriteLine("{0} has not updated in over {1} days!", company.name, maxDays);
            }
        }

        //This runs three test cases: c0 is outdated, c1 has 1 recent update, and c2 has all recent updates
        public static void Main(string[] args)
        {
            //Creates new companies with names and links to the company's site
            Company c0 = new Company("Metabolon", "https://www.metabolon.com/");
            Company c1 = new Company("Amazon", "https://www.amazon.com/");
            Company c2 = new Company("Target", "https://www.target.com/");

            //Create new list of rssfeeds, each including a title, link to a local update file, and the date created
            //metabolon feeds
            List<RSSFeed> f0 = new List<RSSFeed>();
            RSSFeed r0 = new RSSFeed("Title0", "http://localhost/update0", new DateTime(2019, 5, 20));
            f0.Add(r0);
            RSSFeed r1 = new RSSFeed("Title1", "http://localhost/update1", new DateTime(2019, 5, 15));
            f0.Add(r1);
            RSSFeed r2 = new RSSFeed("Title2", "http://localhost/update2", new DateTime(2019, 5, 10));
            f0.Add(r2);

            //amazon feeds
            List<RSSFeed> f1 = new List<RSSFeed>();
            RSSFeed r3 = new RSSFeed("Title0", "http://localhost/update3", new DateTime(2019, 5, 20));
            f1.Add(r3);
            RSSFeed r4 = new RSSFeed("Title1", "http://localhost/update4", new DateTime(2019, 5, 25));
            f1.Add(r4);
            RSSFeed r5 = new RSSFeed("Title2", "http://localhost/update5", new DateTime(2019, 5, 30));
            f1.Add(r5);

            //target feeds
            List<RSSFeed> f2 = new List<RSSFeed>();
            RSSFeed r6 = new RSSFeed("Title0", "http://localhost/update6", new DateTime(2019, 6, 1));
            f2.Add(r6);
            RSSFeed r7 = new RSSFeed("Title1", "http://localhost/update7", new DateTime(2019, 6, 2));
            f2.Add(r7);
            RSSFeed r8 = new RSSFeed("Title2", "http://localhost/update8", new DateTime(2019, 5, 31));
            f2.Add(r8);


            //Create tuples with companies and lists of rssfeeds and max days without update
            Tuple<Company, List<RSSFeed>> cf0 = Tuple.Create(c0, f0); //metabolon
            Tuple<Company, List<RSSFeed>> cf1 = Tuple.Create(c1, f1); //amazon
            Tuple<Company, List<RSSFeed>> cf2 = Tuple.Create(c2, f2); //target

            //Set amount of days that determines whether all the feeds are outdated or not
            double days = 10;

            //Starts Programs with created parameters
            Program p0 = new Program(cf0, days);
            p0.isOutdated();

            Program p1 = new Program(cf1, days);
            p1.isOutdated();

            Program p2 = new Program(cf2, days);
            p2.isOutdated();
        }
    }
}
