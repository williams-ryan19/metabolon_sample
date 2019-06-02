using System;
namespace metabolon_sample
{
    public class RSSFeed
    {
        //Assumption: The RSS feed has already been created and can be accessed as a local file

        public string title; //Title of the new feed object
        public string link;  //Where to pull the feed from
        public DateTime lastUpdate;  //Used to mark the date of the update

        public RSSFeed(string title, string link, DateTime lastUpdate)
        {
            this.title = title;
            this.link = link;
            this.lastUpdate = lastUpdate;
        }
    }
}
