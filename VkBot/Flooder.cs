namespace VkBot
{
    struct Flooder
    {
        public string link { get; set; }

        public string about { get; set; }


        public Flooder(string link, string about)
        {
            this.link = link;
            this.about = about;
        }
    }
}
