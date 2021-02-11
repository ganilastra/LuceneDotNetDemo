namespace SongSearchBL
{
    using NodaTime;
    using System;
    public class Song
    {
        public Song(string title, string artist, Instant releasedDate, string lyrics, Languages language, string jsonData, bool? isAHit = false)
        {
            this.Id = Guid.NewGuid();
            this.ReleasedDate = releasedDate;
            this.Title = title;
            this.Artist = artist;
            this.Lyrics = lyrics;
            this.Language = language;
            this.JsonData = jsonData;
            this.IsAHit = isAHit;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public string Lyrics { get; private set; }
        public Languages Language { get; private set; }
        public bool? IsAHit { get; private set; }
        public string JsonData { get; private set; }

        public Instant ReleasedDate
        {
            get { return Instant.FromUnixTimeTicks(this.ReleasedDateTicksSinceEpoch); }
            set { this.ReleasedDateTicksSinceEpoch = value.ToUnixTimeTicks(); }
        }
        public long ReleasedDateTicksSinceEpoch { get; private set; }
    }
}
