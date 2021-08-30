using SimpleBackendGame.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBackendGame
{
    public class GameDbSeeder
    {
        private readonly GameDbContext _dbContext;

        public GameDbSeeder(GameDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.Items.Any())
                {
                    var items = GetItems();
                    _dbContext.Items.AddRange(items);
                    _dbContext.SaveChanges();
                }
                /*if (!_dbContext.Quests.Any())
                {
                    var quests = GetQuests();
                    _dbContext.Quests.AddRange(quests);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Enemies.Any())
                {
                    var enemies = GetEnemis();
                    _dbContext.Enemies.AddRange(enemies);
                    _dbContext.SaveChanges();
                }*/
                if (!_dbContext.Users.Any())
                {
                    var users = GetUsers();
                    _dbContext.Users.AddRange(users);
                    _dbContext.SaveChanges();
                }
            }
            ICollection<Item> GetItems()
            {
                var items = new List<Item>()
                {
                    new Item()
                    {
                        Name = "Wooden Sword",
                        Stat = 0,
                        Slot = "weapon",
                        Price = 10,
                        Art = ""
                    },
                    new Item()
                    {
                        Name = "Wooden Helm",
                        Stat = 0,
                        Slot = "helm",
                        Price = 10,
                        Art = ""
                    },
                    new Item()
                    {
                        Name = "Wooden Chest",
                        Stat = 0,
                        Slot = "chest",
                        Price = 10,
                        Art = ""
                    },
                    new Item()
                    {
                        Name = "Wooden Boots",
                        Stat = 0,
                        Slot = "boots",
                        Price = 10,
                        Art = ""
                    },
                    new Item()
                    {
                        Name = "Iron Sword",
                        Stat = 0,
                        Slot = "weapon",
                        Price = 30,
                        Art = ""
                    },
                    new Item()
                    {
                        Name = "Iron Helm",
                        Stat = 0,
                        Slot = "helm",
                        Price = 30,
                        Art = ""
                    },
                    new Item()
                    {
                        Name = "Iron Chest",
                        Stat = 0,
                        Slot = "chest",
                        Price = 30,
                        Art = ""
                    },
                    new Item()
                    {
                        Name = "Iron Boots",
                        Stat = 0,
                        Slot = "boots",
                        Price = 30,
                        Art = ""
                    },
                    new Item()
                    {
                        Name = "Emerald Sword",
                        Stat = 0,
                        Slot = "weapon",
                        Price = 100,
                        Art = ""
                    },
                    new Item()
                    {
                        Name = "Emerald Helm",
                        Stat = 0,
                        Slot = "helm",
                        Price = 100,
                        Art = ""
                    },
                    new Item()
                    {
                        Name = "Emerald Chest",
                        Stat = 0,
                        Slot = "chest",
                        Price = 100,
                        Art = ""
                    },
                    new Item()
                    {
                        Name = "Emerald Boots",
                        Stat = 0,
                        Slot = "boots",
                        Price = 100,
                        Art = ""
                    }
                };
                return items;
            }
            ICollection<User> GetUsers()
            {
                var users = new List<User>()
                {
                    new User()
                    {
                        Login = "admin",
                        HashedPassword = "AQAAAAEAACcQAAAAEEtdeMrrUoosdfIav0cWJp6j/vXcTQV7TY/FqPU5Oua29TqPtC+VTBPvk6Yfxb+y1Q==",
                        Role = "admin"
                    },
                    new User()
                    {
                        Login = "moderator",
                        HashedPassword = "AQAAAAEAACcQAAAAEEtdeMrrUoosdfIav0cWJp6j/vXcTQV7TY/FqPU5Oua29TqPtC+VTBPvk6Yfxb+y1Q==",
                        Role = "moderator"
                    },
                    new User()
                    {
                        Login = "user",
                        HashedPassword = "AQAAAAEAACcQAAAAEEtdeMrrUoosdfIav0cWJp6j/vXcTQV7TY/FqPU5Oua29TqPtC+VTBPvk6Yfxb+y1Q=="
                    }
                };
                return users;
            }
            ICollection<Enemy> GetEnemies()
            {
                var enemies = new List<Enemy>()
                {
                    new Enemy()
                    {
                        Id = 1,
                        Name = "Angry Onion",
                        Hp = 50,
                        Atk = 10,
                        Def = 1,
                        Crit = 5,
                        Speed = 5,
                        Art = "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"
                    },
                    new Enemy()
                    {
                        Id = 2,
                        Name = "Angrier Onion",
                        Hp = 50,
                        Atk = 5,
                        Def = 5,
                        Crit = 0,
                        Speed = 0,
                        Art = "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"
                    },
                    new Enemy()
                    {
                        Id = 3,
                        Name = "The Angriest Onion",
                        Hp = 50,
                        Atk = 15,
                        Def = 0,
                        Crit = 0,
                        Speed = 0,
                        Art = "iVBORw0KGgoAAAANSUhEUgAAAIAAAACACAIAAABMXPacAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAACb5JREFUeF7tmcuVJUUMRMcQ2LHFX3zADNYYgQHYMMSriCeylD9l1vd11z1xznRnKZVSxDAbfvx8uJQngIt5AriYJ4CLOTWAHz+evD1PABdzniNwn+j3h4ULAgA6ergqAKDTb88TwMWcaoS8f6PT782VAQB9uJoLh7k4AKLPp6PnM/T5FG4RAFDFWejVHqo+krsEYKj0MPTMm3/+/J36awE/6MMaXT6A2wVAdGFX1PqNWU/3018p1SWo0a7cNABDNzejdgupy/YXvyEU6OaCOu7E3QMw1GIE3UzInXUnRbFMLRb0wB6cGgDQBhtQoyYqXeM8HXLfxFZ6pkek8vMCSFHTNzpNSL1LtcV9oPd6RIrPDgBwrIN4/cVe/slOjUvFAndYlCtjf6A1eqj6+wRAZ0HqmlP7aypXqTfG3Qc6qnBBAECj7cfifMfcSA2VV/IVTR+A9URHFa4JgGjAWWhNblZNwTLIVeq9qb/+QKcVrgwAaMYwqS/QYv6krTXlZXxaE/dgsaHTOhcHQDRsHecItDgftR4KFudlmiAWgEoT9KHO9QFo0hLOC2pxfsB6KH4lL+MkmrUOy3L0uc5lAWjAEs4Cp6CVqabdhziShi7BgiKqaHJqAJorw7aFBcROnNpfawpeKZZpxMxNnTZRaZOTAtBEa9yqi7ctp7oFRQWvNMo07jhavsnhAWiWN2430+Jtx6luQa7gleXxVqWmH0H79zgwAA3yxq1k6i5PRWqcgp2hbpl2GEEu9DgqAE2x4JZJtaNHueKdI5VaJoZcCHBIAJqiaT0U3ByKV6aKN3cnRWmlAHIhxv4BaIqe+1Dc00PdjzfXYk3kQpidA9AUe7sfLzYFrwx11m4VZMEgula832haqydu7lyLpdXNXZMhj6jglYnOnC1Fy88yE0DjHLiJi2pvzj4AP6NywqbglYnOmmyz78ZwAMvr1XPgJs4VWdtaTXgERW5NdMYVwNm09mZmAtBPCa+JFtzERUU2V7vl/7S4T10Fr7TL8LQ7gXiFg2nzzYwFUHv4NdF+7lPxnk6RJ1BTK+O7xH3iFX3bKYMdAnjNspDOWlRt56LUdDyDyCvT7lMs0P7b+L9L3tGd1J58zRKzaSgAKN7ZFHQ/L+NbxH2i0iss0/7biAbQeO81y7YA/v3lNyo9RDE7g/S8rdoTqVyN3niTfjLVrsiCDWwN4DXFQjpfUV33XQasDzanIu5DVsbmhhXkyjvzilzYwKqF68g3iI4y+NUNV9RQALlH/LWteABq+sYV5DopAMC+hk7rsMwNl6thTTsAKP5E45VUbEjcp5ryzro/kkGxvnU/0p1N3XC5GtZ0Awiqe4WjGu5rQ7XO7CMjmjQqOwHUrhEWADdZriFDJ9yHhgJwnyiL36kdAJAdJVRRr2ldBo2bgK3dWLmwQNedVEPF1Pb++X+FpkZzOiA7SrS/gpMCcCdt3SqAdmc6IDtKtL+CzmdQa7E8/cLNlGu7O22NXtk9ACBTMhqfSD8AUOyyvNt3H/rCAUD0QaZkdD+FAgDLK6tinrhpivqIAGpqF+MrfZApGbVPdisaAHC92MINlGvHbYs6+kqjGJ8AfZApGcVP6eFAAGB5a4WbKVdjgVxDxdTRVxrF/EQfZFBG+omVQL8vjAVA1OYG//6gfvQKNHSlVmzntELWlGAB0O9rZgIA7GjT1DS66lA9NFpPDd2qFds5rZAvJTpf9ecgfNWmqWl61SP6m6anMuWjypcMfmoV6M9BlkcPDwDYSVFD/U3TU5nyUeXLmvRTtUZ/DsLWNkRR03uyObHDooaeME0PZsqnlS8J7tz9atw9AGDnuYaeME0PVjzhkPJlgSdAv7/R6YKO5gJQj4MD+PuPX/kDsE9OQ0+YpgcrnmjENXIqwHwA6RBFTe/J/gigm0H8ibTJ9GDFE3YGcmeQjwnAsDKo/YQurMH59GD5rxQ7y51BbhEAKtNi9q8FkIJb+imGPWdvdeWKi3fZXO4McpcA0l/ZPw8gEonDXWH/JwCvSAD8tS2VJqTn1v+gAIAMGuEzAuDPXbHYcIfWf/cAID4hg0a4PoC8jP1TW+3nrniXpCdp/yeAlboBBMVbDjtP+z8BrLQ9ANa3Sfs/Aay0MQAWk39/+c1+sJ+B678lgOIJxIdk0Aj3DQA4r4tiJR2n6enPwDWHngBWauzjvM7FMmCmm/ThCaCrxj7AOe7EGmc9xHPg2lJfJADghnDaEgCkN5oZsGDIfWhjAFB+yBdl0AgzdwDfc0M4bQwA4ivA+U7p2zoAHTVnewJYqV3Gh4BzH+K58524Jk5PACt1y/hWHHc91/YAIHfOp+XOCBcHgJpIGcQXG7j6hoIvUrVid84Z5M4ImwIA6RBOkT2HvHCavjt0sVbszumG3BlhMgDAJ9MhnCJ7TpsITd8dulgrdud0Q9aMsDUAkM6RKrLntInQ9N2hi43i9BOtkDUjzAcA+KoN4RTZc9pEaPru0MVGcfqJVsiXEZ4AOmoUp59ohXwZ4Qmgo3Yxv9IHIF9G2CEAkM5kiuw5bSI0fXfoYruYX2mCTBlkUwCAb6czmSJ7nmOi09DddjG/0gQ5MsgTQEftYnwFNEGODLJPAMBNBrVHpyI1RU1fhIbudotRQAfkyCBbAwB83o0FRfac9nH6IjR0t1uMgmn3wW4BgHwyd5Jr2sfpi9DQ3W4xd5cX4+wQAOAQbrLbBgDFrz8BlPUEsIJDuMmeACLsGQBIJ/sOAWjtywMAnCMd7s4BQMEOjTKuDGTBFDsHAGy+bxKA9p9ltwAAB7L5ngAi7B8A4HzbN2zoCaAMZwKY72sHoD3vFgDgWJwysuRVAUDTGXBHoJ03cFQAAINi9O6S3YKapi+agh3yMi6ohbexfwCA8xGbvrHtnJVzt1LFO7hKrqZtt3FIAIAjAoxO0gWc2l9rmrvlFGziyriaVt3GUQEATgkwfXvP9teGpi+agh1eC7wrtdUXC6BdUNPcrVTxDsuML7iUltzMgQEAzgrcMrm4mzvsauKK02gH1HMjbbiZYwMAHBe4TXJ9RABa5oMCABq5l8GcmydnwEW02B6cEQDg3MStZHoCOBxOT9xi0FUBQMEmGv1zAwDaIMHW+5QAtMlOnB2AwWUI17t5AJr1ywRAtNPCRwSguffj4gAAF0txm7e1SwBQu48m+5IBEO2X4Cyo6YQANNAB7oO7BJCidRecF7lo3PYYIgEAjbgfdwwAaN0EZ4oJxhF3PqG8id5+o+F25aYBGFp9jbNpxwCA3sjQQHtz9wAM2ZBh3pmP0yoGoOcP42MCMGTMkeilU/i8ABzybCfU9EQ+PoBP5wngYp4ALuYJ4FJ+/vwPa7sjq19zxBcAAAAASUVORK5CYII="
                    }
                };
                return enemies;
            }
            ICollection<Quest> GetQuests()
            {
                var quests = new List<Quest>()
                {
                    new Quest()
                    {
                        EnemyId = 1,
                        GoldReward = 20,
                        Map = 1
                    },
                    new Quest()
                    {
                        EnemyId = 2,
                        GoldReward = 25,
                        Map = 1
                    },
                    new Quest()
                    {
                        EnemyId = 3,
                        GoldReward = 30,
                        Map = 1
                    }
                };
                return quests;
            }
        }
    }
}
