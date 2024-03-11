using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YourMatchTgBot.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: false),
                    TranslatedName = table.Column<string>(type: "TEXT", nullable: false),
                    TranslatedDisplayName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    State = table.Column<int>(type: "INTEGER", nullable: false),
                    LanguageCode = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Age = table.Column<short>(type: "INTEGER", nullable: true),
                    Gender = table.Column<int>(type: "INTEGER", nullable: true),
                    InterestsFlags = table.Column<int>(type: "INTEGER", nullable: false),
                    TemporaryInterestsFlags = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: true),
                    CityId = table.Column<long>(type: "INTEGER", nullable: true),
                    Latitude = table.Column<double>(type: "REAL", nullable: true),
                    Longitude = table.Column<double>(type: "REAL", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    PartnerGender = table.Column<int>(type: "INTEGER", nullable: true),
                    ZodiacSign = table.Column<string>(type: "TEXT", nullable: true),
                    Education = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InterestUser",
                columns: table => new
                {
                    InterestsId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestUser", x => new { x.InterestsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_InterestUser_Interests_InterestsId",
                        column: x => x.InterestsId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterestUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempInterest",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    InterestId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempInterest", x => new { x.UserId, x.InterestId });
                    table.ForeignKey(
                        name: "FK_TempInterest_Interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempInterest_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempUserMedia",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    MediaFileId = table.Column<string>(type: "TEXT", nullable: false),
                    MediaType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempUserMedia", x => new { x.UserId, x.MediaFileId });
                    table.ForeignKey(
                        name: "FK_TempUserMedia_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMedia",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    MediaFileId = table.Column<string>(type: "TEXT", nullable: false),
                    MediaType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMedia", x => new { x.UserId, x.MediaFileId });
                    table.ForeignKey(
                        name: "FK_UserMedia_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "DisplayName", "Name", "TranslatedDisplayName", "TranslatedName" },
                values: new object[,]
                {
                    { 150993485L, "Saint Petersburg, Northwestern Federal District, Russia", "Saint Petersburg", "Санкт-Петербург, Северо-Западный федеральный округ, Россия", "Санкт-Петербург" },
                    { 170978781L, "Kursk, Kursk Oblast, Central Federal District, 305000, Russia", "Kursk", "Курск, Курская область, Центральный федеральный округ, 305000, Россия", "Курск" },
                    { 171488446L, "Belgorod, Belgorodsky District, Belgorod Oblast, Central Federal District, Russia", "Belgorod", "Белгород, Белгородский район, Белгородская область, Центральный федеральный округ, Россия", "Белгород" },
                    { 172182020L, "Voronezh, Voronezh Oblast, Central Federal District, Russia", "Voronezh", "Воронеж, Воронежская область, Центральный федеральный округ, Россия", "Воронеж" },
                    { 172497247L, "Penza, Penza Oblast, Volga Federal District, Russia", "Penza", "Пенза, Пензенская область, Приволжский федеральный округ, Россия", "Пенза" },
                    { 174159235L, "Bryansk, Bryansk Oblast, Central Federal District, Russia", "Bryansk", "Брянск, Брянская область, Центральный федеральный округ, Россия", "Брянск" },
                    { 174706474L, "Moscow, Central Federal District, Russia", "Moscow", "Москва, Центральный федеральный округ, Россия", "Москва" },
                    { 177211127L, "Izhevsk, Udmurtia, Volga Federal District, Russia", "Izhevsk", "Ижевск, Удмуртия, Приволжский федеральный округ, Россия", "Ижевск" },
                    { 179737040L, "Samara, Samara Oblast, Volga Federal District, 443028, Russia", "Samara", "Самара, Самарская область, Приволжский федеральный округ, 443028, Россия", "Самара" }
                });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "📚" },
                    { 2, "🎲" },
                    { 4, "🚶" },
                    { 8, "💃" },
                    { 16, "🎞" },
                    { 32, "🏅" },
                    { 64, "💻" },
                    { 128, "🚙" },
                    { 256, "🏔" },
                    { 512, "🍲" },
                    { 1024, "🎧" },
                    { 2048, "🍳" },
                    { 4096, "🛍" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "CityId", "Description", "Education", "Gender", "Height", "InterestsFlags", "LanguageCode", "Latitude", "Longitude", "Name", "PartnerGender", "State", "TemporaryInterestsFlags", "ZodiacSign" },
                values: new object[,]
                {
                    { -65L, (short)17, 172182020L, "﻿Ничто так не разочаровывает, как исполнение наших желаний.", null, 2, 204, 2068, null, 51.660598200000003, 39.200585799999999, "﻿LunaFae", 3, 0, 0, null },
                    { -64L, (short)33, 171488446L, "﻿Удвоенное желание есть страсть, удвоенная страсть становится безумием.", null, 2, 203, 322, null, 50.5955595, 36.587339399999998, "﻿EnchantingEmber", 2, 0, 0, null },
                    { -63L, (short)22, 171488446L, "﻿Обидно, когда твои мечты сбываются у других!", null, 2, 202, 800, null, 50.5955595, 36.587339399999998, "Ditha", 1, 0, 0, null },
                    { -62L, (short)17, 150993485L, "﻿То, чего хочется, всегда кажется необходимым.", null, 2, 201, 560, null, 59.938732000000002, 30.316229, "﻿BlossomBreeze", 3, 0, 0, null },
                    { -61L, (short)35, 174706474L, "﻿Несбыточные желания называют «благими». Как видно, считается, что осуществимы лишь неблагие желания.", null, 2, 200, 322, null, 55.750541200000001, 37.617478200000001, "Елизавета", 2, 0, 0, null },
                    { -60L, (short)24, 179737040L, "﻿Чаще всего хорошей мечтой является та, в которую очень сложно поверить.", null, 2, 199, 704, null, 53.198627000000002, 50.113987000000002, "Амелия", 1, 0, 0, null },
                    { -59L, (short)16, 177211127L, "﻿Один важный секрет: нужно идти туда, куда хочется, а не туда, куда якобы надо.", null, 2, 198, 1041, null, 56.860517450000003, 53.197730742455306, "Алисия", 3, 0, 0, null },
                    { -58L, (short)16, 172497247L, "﻿Люди редко знают, чего хотят, пока не получат того, чего требуют.", null, 2, 197, 1288, null, 53.193783600000003, 45.006741250609664, "Диана", 2, 0, 0, null },
                    { -57L, (short)30, 174159235L, "Ты не перестаешь искать силы и уверенность вовне, а искать следует в себе. Они там всегда и были.", null, 2, 196, 81, null, 53.2423778, 34.3668288, "Анна", 1, 0, 0, null },
                    { -56L, (short)26, 170978781L, "﻿Минимум желаемого — это максимум возможного.", null, 2, 195, 2368, null, 51.727035649999998, 36.192247956921115, "Мария", 1, 0, 0, null },
                    { -55L, (short)17, 174706474L, "﻿Слишком многие у нас живут не работая; и почти столько же — работают не живя.", null, 1, 194, 2084, null, 55.750541200000001, 37.617478200000001, "﻿expensive ▌Г ▌р ▌У ▌с ▌Т ▌ь ▌", 2, 0, 0, null },
                    { -54L, (short)33, 179737040L, "﻿Мужчины взрослеют к шестидесяти годам, женщины — примерно к пятнадцати.", null, 1, 193, 104, null, 53.198627000000002, 50.113987000000002, "Coolfire ПяткаСлона", 1, 0, 0, null },
                    { -53L, (short)27, 177211127L, "﻿Он делал непонятно что, но делал это отлично.", null, 1, 192, 7, null, 56.860517450000003, 53.197730742455306, "﻿dinosaur", 3, 0, 0, null },
                    { -52L, (short)17, 172497247L, "﻿Если никто не знает, что именно вы делаете, никто не знает, что вы делаете это не так.", null, 1, 191, 2068, null, 53.193783600000003, 45.006741250609664, "Bine", 2, 0, 0, null },
                    { -51L, (short)17, 174159235L, "﻿Лучшая работа — это высокооплачиваемое хобби.", null, 1, 190, 532, null, 53.2423778, 34.3668288, "Zadam", 1, 0, 0, null },
                    { -50L, (short)38, 170978781L, "﻿Когда чувствуешь уныние, ищи исцеление в труде.", null, 1, 189, 832, null, 51.727035649999998, 36.192247956921115, "Bbyaakod", 3, 0, 0, null },
                    { -49L, (short)23, 172182020L, "﻿Если бы у меня было восемь часов на то, чтобы срубить дерево, я потратил бы шесть часов на то, чтобы наточить топор.", null, 1, 188, 49, null, 51.660598200000003, 39.200585799999999, "Chelan", 2, 0, 0, null },
                    { -48L, (short)17, 171488446L, "﻿Я путешествую не для того, чтобы приехать куда-то, но чтобы ехать. Главное — это движение.", null, 1, 187, 584, null, 50.5955595, 36.587339399999998, "Arnyack", 1, 0, 0, null },
                    { -47L, (short)35, 171488446L, "﻿Чем позже ты поедешь в Париж, тем старше и достопримечательнее будет собор Парижской Богоматери.", null, 1, 186, 328, null, 50.5955595, 36.587339399999998, "Максим", 1, 0, 0, null },
                    { -46L, (short)28, 150993485L, "﻿Лучшее путешествие — то, которое не имеет завершения.", null, 1, 185, 26, null, 59.938732000000002, 30.316229, "Иван", 3, 0, 0, null },
                    { -45L, (short)17, 174159235L, "Человек всегда делает лишь то, что хочет, и делает это все-таки по необходимости.", null, 2, 184, 1408, null, 53.2423778, 34.3668288, "Олеся", 1, 0, 0, null },
                    { -44L, (short)16, 170978781L, "﻿Нет такого желания, которого нельзя было бы загадагь.", null, 2, 183, 776, null, 51.727035649999998, 36.192247956921115, "Анастасия", 3, 0, 0, null },
                    { -43L, (short)38, 172182020L, "﻿Если это кофе, пожалуйста, принесите мне чаю, а если это чай, пожалуйста, принесите мне кофе.", null, 2, 182, 530, null, 51.660598200000003, 39.200585799999999, "Алиса", 2, 0, 0, null },
                    { -42L, (short)21, 171488446L, "﻿Удивительно, как много у нас друзей, пока не понадобится хотя бы один.", null, 2, 181, 161, null, 50.5955595, 36.587339399999998, "Вера", 1, 0, 0, null },
                    { -41L, (short)16, 171488446L, "﻿Если вы хотите, чтобы жизнь улыбалась вам, подарите ей сначала свое хорошее настроение", null, 2, 180, 296, null, 50.5955595, 36.587339399999998, "Петрова Алёна", 3, 0, 0, null },
                    { -40L, (short)33, 150993485L, "﻿Не жалуйся на жизнь — кто-то мечтает о такой жизни, какой ты живешь", null, 2, 179, 336, null, 59.938732000000002, 30.316229, "Коновалова Вера", 2, 0, 0, null },
                    { -39L, (short)21, 174706474L, "﻿Никогда не делает ошибок в жизни только тот, кто не пробует ничего нового", null, 2, 178, 2208, null, 55.750541200000001, 37.617478200000001, "Комарова Эмилия", 1, 0, 0, null },
                    { -38L, (short)17, 179737040L, "﻿В жизни нет безвыходных ситуаций, есть только непринятые решения", null, 2, 177, 52, null, 53.198627000000002, 50.113987000000002, "Сафонова Ксения", 1, 0, 0, null },
                    { -37L, (short)38, 177211127L, "﻿Не тот велик, кто никогда не падал, а тот велик — кто падал и вставал!", null, 2, 176, 1544, null, 56.860517450000003, 53.197730742455306, "Ершова Арина", 3, 0, 0, null },
                    { -36L, (short)24, 172497247L, "Пессимист видит трудности при каждой возможности; оптимист в каждой трудности видит возможности", null, 2, 175, 1048, null, 53.193783600000003, 45.006741250609664, "Евсеева Александра", 2, 0, 0, null },
                    { -35L, (short)19, 171488446L, "﻿Добрые поступки не выходят за ворота; дурные — путешествуют на тысячу ри.", null, 1, 174, 100, null, 50.5955595, 36.587339399999998, "Денис", 3, 0, 0, null },
                    { -34L, (short)17, 150993485L, "﻿Как все великие путешественники, я видел больше, чем помню, и помню больше, чем видел.", null, 1, 173, 1034, null, 59.938732000000002, 30.316229, "Артемий", 2, 0, 0, null },
                    { -33L, (short)37, 174706474L, "﻿Жить — значит меняться, меняться — значит взрослеть, а взрослеть — значит непрестанно творить себя самого.", null, 1, 172, 134, null, 55.750541200000001, 37.617478200000001, "Дмитрий", 1, 0, 0, null },
                    { -32L, (short)24, 179737040L, "﻿Нынешняя молодежь так быстро взрослеет, что сознательно затягивает стадию инфантилизма.", null, 1, 171, 146, null, 53.198627000000002, 50.113987000000002, "Александр", 3, 0, 0, null },
                    { -31L, (short)16, 177211127L, "﻿Кто не стучится — тому не открывают. Кто не пробует — у того не получается", null, 1, 170, 5276, null, 56.860517450000003, 53.197730742455306, "Фёдор", 2, 0, 0, null },
                    { -30L, (short)31, 172497247L, "﻿Подлинным зеркалом нашего образа мыслей является наша жизнь", null, 1, 169, 1092, null, 53.193783600000003, 45.006741250609664, "Кирилл", 1, 0, 0, null },
                    { -29L, (short)28, 174159235L, "﻿Перемены, происходящие в нашей жизни, есть следствие нашего выбора и наших решений", null, 1, 168, 1057, null, 53.2423778, 34.3668288, "Илья", 1, 0, 0, null },
                    { -28L, (short)16, 170978781L, "﻿Нужно не тратить время, а инвестировать в него", null, 1, 167, 1600, null, 51.727035649999998, 36.192247956921115, "Лукьянов Александр", 3, 0, 0, null },
                    { -27L, (short)16, 172182020L, "﻿Никто не изготовит замок без ключа, также и жизнь не даст проблемы без решения", null, 1, 166, 2192, null, 51.660598200000003, 39.200585799999999, "Иванов Илья", 2, 0, 0, null },
                    { -26L, (short)37, 171488446L, "﻿О нравственных качествах человека нужно судить не по отдельным его усилиям, а по его повседневной жизни", null, 1, 165, 392, null, 50.5955595, 36.587339399999998, "Григорьев Гордей", 1, 0, 0, null },
                    { -25L, (short)24, 177211127L, "﻿Никогда не осуждайте человека, пока не пройдете долгий путь в его ботинках", null, 2, 164, 280, null, 56.860517450000003, 53.197730742455306, "Зайцева Валерия", 2, 0, 0, null },
                    { -24L, (short)17, 172497247L, "﻿Если все сложилось не так, как вы ожидали, не расстраивайтесь. Божьи планы всегда лучше наших", null, 2, 163, 1664, null, 53.193783600000003, 45.006741250609664, "Киселева Варвара", 1, 0, 0, null },
                    { -23L, (short)31, 174159235L, "﻿Музыканты рисуют свои картины на фоне тишины. Мы даем музыку, а вы даете тишину.", null, 2, 162, 38, null, 53.2423778, 34.3668288, "Лукьянова Анастасия", 3, 0, 0, null },
                    { -22L, (short)21, 170978781L, "﻿Современная музыка хороша уже тем, что, если ты ошибешься, никто не заметит.", null, 2, 161, 74, null, 51.727035649999998, 36.192247956921115, "Куликова Виктория", 2, 0, 0, null },
                    { -21L, (short)16, 171488446L, "Хороший инвестор — всё равно что прилежный студент. Каждый день я трачу часы на чтение финансовой прессы.", null, 2, 160, 1544, null, 50.5955595, 36.587339399999998, "Макарова Валерия Макаровна", 1, 0, 0, null },
                    { -20L, (short)30, 171488446L, "﻿Мы не всегда свободны от ошибок, по поводу которых смеемся над другими.", null, 2, 159, 2113, null, 50.5955595, 36.587339399999998, "Троицкая Мария Артёмовна", 3, 0, 0, null },
                    { -19L, (short)25, 150993485L, "Я понял, что жизнь ничего не стоит, но я также понял, что ничто не стоит жизни.", null, 2, 158, 259, null, 59.938732000000002, 30.316229, "Михеева Аиша Михайловна", 2, 0, 0, null },
                    { -18L, (short)16, 174706474L, "﻿Жизнь измеряется не числом вдохов и выдохов, а числом мгновений, в которые перехватывает дыхание.", null, 2, 157, 69, null, 55.750541200000001, 37.617478200000001, "Федорова Софья Егоровна", 1, 0, 0, null },
                    { -17L, (short)34, 179737040L, "﻿Стремитесь не к тому, чтобы добиться успеха, а к тому, чтобы твоя жизнь имела смысл", null, 2, 156, 642, null, 53.198627000000002, 50.113987000000002, "Вешнякова Валерия Яковлевна", 3, 0, 0, null },
                    { -16L, (short)17, 171488446L, "﻿Красота — это внешность, фото — искусство, а главное в жизни — доброе сердце, характер и чувства", null, 1, 155, 2120, null, 50.5955595, 36.587339399999998, "Степанов Валерий", 2, 0, 0, null },
                    { -15L, (short)33, 150993485L, "﻿Жизнь — не зебра из черных и белых полос, а шахматная доска. Здесь все зависит от твоего хода", null, 1, 154, 296, null, 59.938732000000002, 30.316229, "Кузнецов Серафим", 1, 0, 0, null },
                    { -14L, (short)25, 174706474L, "﻿Кто хочет жить для других, не должен пренебрегать собственной жизнью", null, 1, 153, 2177, null, 55.750541200000001, 37.617478200000001, "Медведев Иван", 1, 0, 0, null },
                    { -13L, (short)16, 179737040L, "﻿Живи так — чтобы люди, столкнувшись с тобой, улыбнулись, а, общаясь с тобой, стали чуточку счастливее", null, 1, 152, 1284, null, 53.198627000000002, 50.113987000000002, "Мальцев Артём", 3, 0, 0, null },
                    { -12L, (short)36, 177211127L, "﻿Быстрее всего учишься в трех случаях — до 7 лет, на тренингах, и когда жизнь загнала тебя в угол", null, 1, 151, 50, null, 56.860517450000003, 53.197730742455306, "Иванов Ярослав Артёмович", 2, 0, 0, null },
                    { -11L, (short)19, 172497247L, "﻿Единственное правило в жизни, по которому нужно жить — оставаться человеком в любых ситуациях", null, 1, 150, 672, null, 53.193783600000003, 45.006741250609664, "Новиков Роман Андреевич", 1, 0, 0, null },
                    { -10L, (short)17, 174159235L, "﻿Чтобы вы ни делали, делайте это хорошо", null, 1, 149, 3073, null, 53.2423778, 34.3668288, "Рудаков Артём Артёмович", 3, 0, 0, null },
                    { -9L, (short)35, 170978781L, "﻿Когда мне тяжело, я всегда напоминаю себе о том, что если я сдамся — лучше не станет", null, 1, 148, 1042, null, 51.727035649999998, 36.192247956921115, "Трифонов Макар Янович", 2, 0, 0, null },
                    { -8L, (short)32, 174706474L, "﻿Удвоенное желание есть страсть, удвоенная страсть становится безумием.", null, 2, 147, 81, null, 55.750541200000001, 37.617478200000001, "Гончарова Ника Артёмовна", 3, 0, 0, null },
                    { -7L, (short)20, 179737040L, "﻿Характер подобен дереву, а репутация — его тени. Мы заботимся о тени, но на самом деле надо думать о дереве.", null, 2, 146, 49, null, 53.198627000000002, 50.113987000000002, "Исаева Полина Максимовна", 2, 0, 0, null },
                    { -6L, (short)17, 177211127L, "﻿Нельзя сдаваться не только после одного, но и после ста поражений.", null, 2, 145, 1057, null, 56.860517450000003, 53.197730742455306, "Костина Ксения Степановна", 1, 0, 0, null },
                    { -5L, (short)33, 172182020L, "﻿Можно всё время дурачить некоторых, можно некоторое время дурачить всех, но нельзя все время дурачить всех.", null, 2, 144, 592, null, 51.660598200000003, 39.200585799999999, "Горшкова Варвара Антоновна", 1, 0, 0, null },
                    { -4L, (short)38, 174159235L, "﻿Грамм собственного опыта стоит дороже тонны чужих наставлений!", null, 1, 143, 1538, null, 53.2423778, 34.3668288, "Сафонов Иван Даниилович", 2, 0, 0, null },
                    { -3L, (short)27, 170978781L, "﻿Каждый день не может быть хорошим, важно лишь запомнить хорошее в каждом дне", null, 1, 142, 104, null, 51.727035649999998, 36.192247956921115, "Харитонов Александр Александрович", 1, 0, 0, null },
                    { -2L, (short)16, 172497247L, "﻿Окружай себя только теми людьми, кто будет тянуть тебя выше. Просто жизнь уже полна теми, кто хочет тянуть тебя вниз", null, 1, 141, 1072, null, 53.193783600000003, 45.006741250609664, "Фролов Евгений Дмитриевич", 3, 0, 0, null },
                    { -1L, (short)19, 172182020L, "﻿В конце жизни важны не прожитые годы, а то, как вы их прожили.", null, 2, 140, 208, null, 51.660598200000003, 39.200585799999999, "Жданова Маргарита Юрьевна", 3, 0, 0, null }
                });

            migrationBuilder.InsertData(
                table: "UserMedia",
                columns: new[] { "MediaFileId", "UserId", "MediaType" },
                values: new object[,]
                {
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -65L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -64L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -63L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -62L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -61L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -60L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -59L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -58L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -57L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -56L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -55L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -54L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -53L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -52L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -51L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -50L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -49L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -48L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -47L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -46L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -45L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -44L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -43L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -42L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -41L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -40L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -39L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -38L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -37L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -36L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -35L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -34L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -33L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -32L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -31L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -30L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -29L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -28L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -27L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -26L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -25L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -24L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -23L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -22L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -21L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -20L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -19L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -18L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -17L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -16L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -15L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -14L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -13L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -12L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -11L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -10L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -9L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -8L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -7L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -6L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -5L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -4L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -3L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -2L, 2 },
                    { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -1L, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterestUser_UsersId",
                table: "InterestUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_TempInterest_InterestId",
                table: "TempInterest",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterestUser");

            migrationBuilder.DropTable(
                name: "TempInterest");

            migrationBuilder.DropTable(
                name: "TempUserMedia");

            migrationBuilder.DropTable(
                name: "UserMedia");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
