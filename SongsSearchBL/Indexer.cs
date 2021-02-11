using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Newtonsoft.Json.Linq;
using NodaTime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SongSearchBL
{
    public static class Indexer
    {
        public static void IndexSongs(string indexDirectory = @"c:\Lucene\Index")
        {
            var songsToIndex = GetSongsToIndex();

            // Setting up the File system folder where to create the index.
            var indexDirectoryInfo = new DirectoryInfo(indexDirectory);
            var lockFactory = new SimpleFSLockFactory();

            using (var writer = new IndexWriter(
               FSDirectory.Open(indexDirectoryInfo, lockFactory),
               new StandardAnalyzer(
                   Lucene.Net.Util.Version.LUCENE_30),
               IndexWriter.MaxFieldLength.UNLIMITED))
            {

                foreach (var song in songsToIndex)
                {
                    var document = new Document();

                    var songId = song.Id.ToString("N");

                    // This FIELD does not need to be analyzed.
                    document.Add(
                      new Field(SongFieldNames.FieldSongId, songId, Field.Store.YES, Field.Index.NOT_ANALYZED));

                    document.Add(
                      new Field(SongFieldNames.FieldTitle, song.Title, Field.Store.YES, Field.Index.ANALYZED));


                    document.Add(
                      new Field(SongFieldNames.FieldArtist, song.Artist, Field.Store.YES, Field.Index.ANALYZED));
                    
                    document.Add(
                        new Field(SongFieldNames.FieldLanguage, Enum.GetName(typeof(Languages), song.Language), Field.Store.YES, Field.Index.NOT_ANALYZED));

                    // The lyrics will be searched and analyzed, but its would not be returned in the search results, that is why its field.store.No
                    document.Add(
                      new Field(SongFieldNames.FieldLyrics, song.Lyrics, Field.Store.NO, Field.Index.ANALYZED));

                    var numReleasedDateField = new NumericField(SongFieldNames.FieldReleaseDate, Field.Store.YES, true);
                    numReleasedDateField.SetLongValue(song.ReleasedDateTicksSinceEpoch);
                    document.Add(numReleasedDateField);

                    if (song.IsAHit == true)
                    {
                        document.Boost = 2.0f;
                    }

                    //QUESTION: How do you add this JSON FIELD, if it needs to be analyzed but not returned in the search results?
                    // var jsonField = @"{label:'Sony Music',Musicians:[{name:'Eddie Van Halen',roles:['composer','guitar','synth']},
                    //                  {name:'David Lee Roth',roles:['composer','vocals']}],Duration: '5:31'}";

                    writer.UpdateDocument(new Term(SongFieldNames.FieldSongId, songId), document);
                }
            }
        }

        public static void DeleteIndexes(string indexDirectory = @"c:\Lucene\Index")
        {
            var indexDirectoryInfo = new DirectoryInfo(indexDirectory);
            indexDirectoryInfo.Delete(true);
        }

        static string FlattenFormDataJsonString(string json)
        {
            var jsonFormData = JToken.Parse(json);
            JObject jsonObject = JObject.FromObject(jsonFormData);
            var flattenedString = jsonObject.Flatten();
            return string.Join(" ", flattenedString.Select(x => x.Value));
        }

        static IList<KeyValuePair<string, string>> Flatten(this JObject jToken)
        {
            return jToken
                .Descendants()
                .Where(p => p.Count() == 0)
                .Aggregate(new List<KeyValuePair<string, string>>(), (properties, leaf) =>
                {
                    properties.Add(new KeyValuePair<string, string>(leaf.Path, leaf.ToString()));
                    return properties;
                });
        }

        static List<Song> GetSongsToIndex()
        {
            List<Song> songs = new List<Song>
            {
                new Song("Panama", "Van Halen", Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(1984, 6, 18), DateTimeKind.Utc)), @"Uh!
                                Oh yeah!
                                Ah-huh!
                                Jump back, what's that sound?
                                Here she comes, full blast'n top down
                                Hot shoe, burnin' down the avenue
                                Model citizen, zero discipline
                                Don't you know she's coming home with me
                                You'll lose her in that turn
                                I'll get her!
                                Panama, Panama
                                Panama, Panama
                                Ain't nothin' like it, it's a shining machine
                                Got the feel for the wheel, keep the movin' parts clean
                                Hot shoe, burnin' down the avenue
                                Got an on-ramp comin' through my bedroom
                                Don't you know she's coming home with me
                                You'll lose her in that turn
                                I'll get her
                                Oh!
                                Panama, Panama
                                Ow!
                                Panama, Panama
                                Oh-oh-oh-oh
                                Woo!
                                Yeah, we're runnin' a little bit hot tonight
                                I can barely see the road from the heat comin'…",
                                Languages.English,  @"{label:'Sony Music',Musicians:[{name:'Eddie Van Halen',roles:['composer','guitar','synth']},
                                        {name:'David Lee Roth',roles:['composer','vocals']}],Duration: '5:31'}"),

                new Song("If Your Happy And You know it", "Joe Raposo", Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(1957, 1, 1), DateTimeKind.Utc)), @"If you're happy and you know it, clap your hands!
                                If you're happy and you know it, clap your hands!
                                If you're happy and you know it, and you really want to show it;
                                If you're happy and you know it, clap your hands
                                jump around!",
                                Languages.English,  @"{label:'unkknown',Musicians:[],Duration: '3:00'}"),


                   new Song("House of the Rising Sun", "The Animals", Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(1964, 5, 5), DateTimeKind.Utc)), 
                   @"There is a house in New Orleans
They call the Rising Sun
And it's been the ruin of many a poor boy
And God, I know I'm one
My mother was a tailor
She sewed my new blue jeans
My father was a gamblin' man
Down in New Orleans
Now the only thing a gambler needs
Is a suitcase and a trunk
And the only time he's satisfied
Is when he's all drunk
Oh mother, tell your children
Not to do what I have done
Spend your lives in sin and misery
In the House of the Rising Sun
Well, I got one foot on the platform
The other foot on the train
I'm goin' back to New Orleans
To wear that ball and chain
Well, there is a house in New Orleans
They call the Rising Sun
And it's been the ruin of many a poor boy
And God, I know I'm one",
                    Languages.English, @"{label:'Keynote Records',Musicians:[{name:'Hilton Valentine',roles:['composer','guitar']},
                                        {name:'Eric Burdon',roles:['composer','vocals']}],Duration: '4:11'}"),


                new Song("Jump Around", "House Of pain", Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(1992, 5, 5), DateTimeKind.Utc)), @"Pack it up, pack it in, let me begin
                    I came to win, battle me that's a sin
                    I won't ever slack up, punk you better back up
                    Try and play the role and yo the whole crew'll act up
                    Get up, stand up (c'mon!) see'mon throw your hands up
                    If you've got the feeling, jump across the ceiling
                    Muggs lifts a funk flow, someone's talking junk
                    Yo I bust him in the eye, and then I'll take the punk's hoe
                    Feelin', funkin', amps in the trunk and I got more rhymes
                    Than there's cops at a Dunkin' Donuts shop
                    Sho' nuff, I got props
                    From the kids on the hill plus my mom and my pops
                    I came to get down, I came to get down
                    So get out your seat and jump around!
                    Jump around!
                    Jump around!
                    Jump around!
                    Jump up, jump up and get down!
                    Jump! Jump! Jump! Jump! (Everybody jump)
                    Jump! Jump! Jump!…",
                    Languages.English,@"{label:'Tommy Boy',Musicians:[{name:'Everlast',roles:['composer','rap']},
                                        {name:'DJ Lethal',roles:['composer','DJ']}],Duration: '3:55'}"),

                     new Song("Besame Mucho", "Consuelo Velasquez", Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(1944, 1, 12), DateTimeKind.Utc)), @"Ay
                            Bésame, bésame mucho
Como si fuera esta la noche
La última vez
Bésame, bésame mucho
Que tengo miedo a perderte
Perderte después
Bésame, bésame mucho
Como si fuera esta la noche
La última vez
Bésame, bésame mucho
Que tengo miedo a perderte
Perderte después
Quiero tenerte muy cerca
Mirarme en tus ojos
Verte junto a mi
Piensa que tal vez mañana
Yo ya estaré lejos
Muy lejos de aquí
Bésame, bésame mucho
Como si fuera esta la noche
La última vez
Bésame, bésame… …",
                    Languages.Spanish, "{}"),


                new Song("Despacito", "Luis Fonsi", Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(2017, 1, 12), DateTimeKind.Utc)), @"Ay
                            Fonsi
                            DY
                            Oh-oh
                            Oh no, oh no (oh)
                            Hey yeah
                            Diridiri, dirididi Daddy
                            Go
                            Sí, sabes que ya llevo un rato mirándote
                            Tengo que bailar contigo hoy (DY)
                            Vi que tu mirada ya estaba llamándome
                            Muéstrame el camino que yo voy
                            Oh
                            Tú, tú eres el imán y yo soy el metal
                            Me voy acercando y voy armando el plan
                            Solo con pensarlo se acelera el pulso
                            Oh yeah
                            Ya, ya me está gustando más de lo normal
                            Todos mis sentidos van pidiendo más
                            Esto hay que tomarlo sin ningún apuro
                            Despacito
                            Quiero respirar tu cuello despacito
                            Deja que te diga cosas al oído
                            Para que te acuerdes si no estás conmigo
                            Despacito
                            Quiero desnudarte a besos despacito
                            Firmar las paredes de tu laberinto
                            Y hacer de tu cuerpo todo un manuscrito (sube, sube, sube)
                            (Sube, sube) Oh
                            Quiero ver bailar tu pelo …",
                    Languages.Spanish, "{}", true),

                new Song("Nandito Ako", "Ogie Alcasid", Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(1988, 1, 1), DateTimeKind.Utc)), @"Mayro'n akong nais malaman
Maaari bang magtanong?
Alam mo bang matagal na kitang iniibig?
Matagal na akong naghihintay
Ngunit mayro'n kang ibang minamahal
Kung kaya't ako'y di mo pinapansin
Ngunit ganon pa man nais kong malaman mo
Ang puso kong ito'y para lang sa 'yo
Nandito ako umiibig sa iyo
Kahit na nagdurugo ang puso
Kung sakaling iwanan ka niya
Huwag kang mag-alala
May nagmamahal sa iyo,
Nandito ako
Kung ako ay iyong iibigin
Di kailangan ang mangamba
Pagkat ako ay para mong alipin
Sa iyo lang wala ng iba
Ngunit mayro'n ka ng ibang minamahal
Kung kaya't ako'y di o pinapansin
Ngunit ganon pa man nais kong malaman mo
Ang puso kong ito'y para lang sa 'yo
Nandito ako umiibig sa iyo
Kahit na nagdurugo ang puso
Kung sakaling iwanan ka niya
Huwag kang mag-alala
May nagmamahal sa iyo,
Nandito ako
Nandito ako umiibig sa iyo
Kahit na nagdurugo ang puso
Kung sakaling iwanan ka niya
Huwag kang mag-alala
May nagmamahal sa iyo,
Nandito ako",
                   Languages.Filipino, "{}"),

                new Song("Bikining Itim", "Bert Dominic", Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(1990, 1, 1), DateTimeKind.Utc)), @"Sa 'yong inaakala nalimot na kita
Gayong ako ay laging tapat sa 'yo sinta
Buhat nang mawala ka ako ay nagdurusa
Kahit ka nagtampo sana'y malaman mo
Mahal pa rin kita
Hinanap ka sa disco, beerhouse at restaurant
Mayro'ng nakapagsabing ika'y nasa Japan
Ako'y nangungulila ng anim na buwan
Dahil mahal kita alang-alang sa 'yo
Magtitiis ako
Ang iniingat-ingatan ko
Kaisa-isang bikini mo
Na tinatago-tago ko pa
At katabi sa pagtulog ko
Ako'y dumadalangin
Na kahit manawari
Huwag sanang sungkitin bikini mong itim
Na alay mo sa 'kin
Ang iniingat-ingatan ko
Kaisa-isang bikini mo
Na tinatago-tago ko pa
At katabi sa pagtulog ko
Ako'y dumadalangin
Na kahit manawari
Huwag sanang sungkitin bikini mong itim
Na alay mo sa 'kin
Huwag sanang sungkitin bikini mong itim
Na alay mo sa 'kin
Huwag sanang sungkitin bikini mong itim
Na alay mo sa 'kin",
                   Languages.Filipino, "{}")
            };


            return songs;
        }
    }
}
