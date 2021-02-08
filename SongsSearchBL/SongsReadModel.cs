namespace SongSearchBL
{
    using Lucene.Net.Documents;
    using NodaTime;
    using System;
    public class SongsReadModel
    {
        public SongsReadModel(Document document)
        {
            this.Id = Guid.Parse(document.Get(SongFieldNames.FieldSongId));
            this.ReleasedDateTicksSinceEpoch = long.Parse(document.Get(SongFieldNames.FieldReleaseDate));
            this.Title = document.Get(SongFieldNames.FieldTitle);
            this.Artist = document.Get(SongFieldNames.FieldArtist);
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public Instant ReleasedDate
        {
            get { return Instant.FromUnixTimeTicks(this.ReleasedDateTicksSinceEpoch); }
            set { this.ReleasedDateTicksSinceEpoch = value.ToUnixTimeTicks(); }
        }
        private long ReleasedDateTicksSinceEpoch { get; set; }
    }
}
