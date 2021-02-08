namespace SongSearchBL
{
    using NodaTime;
    using System;
    public class SearchParameters
    {
        public string[] Terms { get; set; }
        public Languages Language { get; set; }

        public Instant DateFrom
        {
            get { return Instant.FromUnixTimeTicks(this.DateFromTicksSinceEpoch); }
            set { this.DateFromTicksSinceEpoch = value.ToUnixTimeTicks(); }
        }
        public long DateFromTicksSinceEpoch { get; private set; }


        public Instant DateTo
        {
            get { return Instant.FromUnixTimeTicks(this.DateToTicksSinceEpoch); }
            set { this.DateToTicksSinceEpoch = value.ToUnixTimeTicks(); }
        }
        public long DateToTicksSinceEpoch { get; private set; }
    }
}
