namespace IRZTask2
{
    class Question
    {
        public Item[] items { get; set; }
    }
    class Item
    {
        public Owner owner { get; set; }
        public string creation_date { get; set; }
        public string title { get; set; }
        public bool is_answered { get; set; }
        public string link { get; set; }
    }
    class Owner
    {
        public string display_name { get; set; }
        public string link { get; set; }
    }
}
