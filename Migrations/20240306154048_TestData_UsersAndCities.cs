using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YourMatchTgBot.Migrations
{
    /// <inheritdoc />
    public partial class TestData_UsersAndCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "DisplayName", "Name" },
                values: new object[,]
                {
                    { 150993485L, "Saint Petersburg, Northwestern Federal District, Russia", "Saint Petersburg" },
                    { 171572505L, "Belgorod, Belgorodsky District, Belgorod Oblast, Central Federal District, Russia", "Belgorod" },
                    { 171830246L, "Kursk, Kursk Oblast, Central Federal District, 305000, Russia", "Kursk" },
                    { 171835519L, "Stroitel, Yakovlevsky District, Belgorod Oblast, Central Federal District, Russia", "Stroitel" },
                    { 172182020L, "Voronezh, Voronezh Oblast, Central Federal District, Russia", "Voronezh" },
                    { 174007564L, "Penza, Penza Oblast, Volga Federal District, Russia", "Penza" },
                    { 174159235L, "Bryansk, Bryansk Oblast, Central Federal District, Russia", "Bryansk" },
                    { 174912365L, "Lipetsk, Lipetsk Oblast, Central Federal District, 398000, Russia", "Lipetsk" },
                    { 176023942L, "Izhevsk, Udmurtia, Volga Federal District, Russia", "Izhevsk" },
                    { 179737040L, "Samara, Samara Oblast, Volga Federal District, 443028, Russia", "Samara" },
                    { 206154094L, "Moscow, Central Federal District, Russia", "Moscow" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "CityId", "Description", "Education", "Gender", "Height", "InterestsFlags", "LanguageCode", "Name", "PartnerGender", "State", "TemporaryInterestsFlags", "ZodiacSign" },
                values: new object[,]
                {
                    { -65L, (short)17, 172182020L, "﻿Ничто так не разочаровывает, как исполнение наших желаний.", null, 2, 204, 321, null, "﻿LunaFae", 3, 0, 0, null },
                    { -64L, (short)33, 171572505L, "﻿Удвоенное желание есть страсть, удвоенная страсть становится безумием.", null, 2, 203, 3200, null, "﻿EnchantingEmber", 2, 0, 0, null },
                    { -63L, (short)22, 171572505L, "﻿Обидно, когда твои мечты сбываются у других!", null, 2, 202, 2065, null, "Ditha", 1, 0, 0, null },
                    { -62L, (short)17, 150993485L, "﻿То, чего хочется, всегда кажется необходимым.", null, 2, 201, 35, null, "﻿BlossomBreeze", 3, 0, 0, null },
                    { -61L, (short)35, 206154094L, "﻿Несбыточные желания называют «благими». Как видно, считается, что осуществимы лишь неблагие желания.", null, 2, 200, 656, null, "Елизавета", 2, 0, 0, null },
                    { -60L, (short)24, 179737040L, "﻿Чаще всего хорошей мечтой является та, в которую очень сложно поверить.", null, 2, 199, 289, null, "Амелия", 1, 0, 0, null },
                    { -59L, (short)16, 176023942L, "﻿Один важный секрет: нужно идти туда, куда хочется, а не туда, куда якобы надо.", null, 2, 198, 2368, null, "Алисия", 3, 0, 0, null },
                    { -58L, (short)16, 174007564L, "﻿Люди редко знают, чего хотят, пока не получат того, чего требуют.", null, 2, 197, 162, null, "Диана", 2, 0, 0, null },
                    { -57L, (short)30, 174159235L, "Ты не перестаешь искать силы и уверенность вовне, а искать следует в себе. Они там всегда и были.", null, 2, 196, 97, null, "Анна", 1, 0, 0, null },
                    { -56L, (short)26, 171830246L, "﻿Минимум желаемого — это максимум возможного.", null, 2, 195, 13, null, "Мария", 1, 0, 0, null },
                    { -55L, (short)17, 206154094L, "﻿Слишком многие у нас живут не работая; и почти столько же — работают не живя.", null, 1, 194, 386, null, "﻿expensive ▌Г ▌р ▌У ▌с ▌Т ▌ь ▌", 2, 0, 0, null },
                    { -54L, (short)33, 179737040L, "﻿Мужчины взрослеют к шестидесяти годам, женщины — примерно к пятнадцати.", null, 1, 193, 2116, null, "Coolfire ПяткаСлона", 1, 0, 0, null },
                    { -53L, (short)27, 176023942L, "﻿Он делал непонятно что, но делал это отлично.", null, 1, 192, 2308, null, "﻿dinosaur", 3, 0, 0, null },
                    { -52L, (short)17, 174007564L, "﻿Если никто не знает, что именно вы делаете, никто не знает, что вы делаете это не так.", null, 1, 191, 1033, null, "Bine", 2, 0, 0, null },
                    { -51L, (short)17, 174159235L, "﻿Лучшая работа — это высокооплачиваемое хобби.", null, 1, 190, 11, null, "Zadam", 1, 0, 0, null },
                    { -50L, (short)38, 171830246L, "﻿Когда чувствуешь уныние, ищи исцеление в труде.", null, 1, 189, 518, null, "Bbyaakod", 3, 0, 0, null },
                    { -49L, (short)23, 172182020L, "﻿Если бы у меня было восемь часов на то, чтобы срубить дерево, я потратил бы шесть часов на то, чтобы наточить топор.", null, 1, 188, 800, null, "Chelan", 2, 0, 0, null },
                    { -48L, (short)17, 171572505L, "﻿Я путешествую не для того, чтобы приехать куда-то, но чтобы ехать. Главное — это движение.", null, 1, 187, 2066, null, "Arnyack", 1, 0, 0, null },
                    { -47L, (short)35, 171572505L, "﻿Чем позже ты поедешь в Париж, тем старше и достопримечательнее будет собор Парижской Богоматери.", null, 1, 186, 1664, null, "Максим", 1, 0, 0, null },
                    { -46L, (short)28, 150993485L, "﻿Лучшее путешествие — то, которое не имеет завершения.", null, 1, 185, 88, null, "Иван", 3, 0, 0, null },
                    { -45L, (short)17, 174159235L, "Человек всегда делает лишь то, что хочет, и делает это все-таки по необходимости.", null, 2, 184, 1027, null, "Олеся", 1, 0, 0, null },
                    { -44L, (short)16, 171830246L, "﻿Нет такого желания, которого нельзя было бы загадагь.", null, 2, 183, 324, null, "Анастасия", 3, 0, 0, null },
                    { -43L, (short)38, 172182020L, "﻿Если это кофе, пожалуйста, принесите мне чаю, а если это чай, пожалуйста, принесите мне кофе.", null, 2, 182, 1408, null, "Алиса", 2, 0, 0, null },
                    { -42L, (short)21, 171572505L, "﻿Удивительно, как много у нас друзей, пока не понадобится хотя бы один.", null, 2, 181, 530, null, "Вера", 1, 0, 0, null },
                    { -41L, (short)16, 171572505L, "﻿Если вы хотите, чтобы жизнь улыбалась вам, подарите ей сначала свое хорошее настроение", null, 2, 180, 385, null, "Петрова Алёна", 3, 0, 0, null },
                    { -40L, (short)33, 150993485L, "﻿Не жалуйся на жизнь — кто-то мечтает о такой жизни, какой ты живешь", null, 2, 179, 2564, null, "Коновалова Вера", 2, 0, 0, null },
                    { -39L, (short)21, 206154094L, "﻿Никогда не делает ошибок в жизни только тот, кто не пробует ничего нового", null, 2, 178, 1042, null, "Комарова Эмилия", 1, 0, 0, null },
                    { -38L, (short)17, 179737040L, "﻿В жизни нет безвыходных ситуаций, есть только непринятые решения", null, 2, 177, 1120, null, "Сафонова Ксения", 1, 0, 0, null },
                    { -37L, (short)38, 176023942L, "﻿Не тот велик, кто никогда не падал, а тот велик — кто падал и вставал!", null, 2, 176, 38, null, "Ершова Арина", 3, 0, 0, null },
                    { -36L, (short)24, 174007564L, "Пессимист видит трудности при каждой возможности; оптимист в каждой трудности видит возможности", null, 2, 175, 2177, null, "Евсеева Александра", 2, 0, 0, null },
                    { -35L, (short)19, 171572505L, "﻿Добрые поступки не выходят за ворота; дурные — путешествуют на тысячу ри.", null, 1, 174, 1344, null, "Денис", 3, 0, 0, null },
                    { -34L, (short)17, 150993485L, "﻿Как все великие путешественники, я видел больше, чем помню, и помню больше, чем видел.", null, 1, 173, 1034, null, "Артемий", 2, 0, 0, null },
                    { -33L, (short)37, 206154094L, "﻿Жить — значит меняться, меняться — значит взрослеть, а взрослеть — значит непрестанно творить себя самого.", null, 1, 172, 2081, null, "Дмитрий", 1, 0, 0, null },
                    { -32L, (short)24, 179737040L, "﻿Нынешняя молодежь так быстро взрослеет, что сознательно затягивает стадию инфантилизма.", null, 1, 171, 2082, null, "Александр", 3, 0, 0, null },
                    { -31L, (short)16, 176023942L, "﻿Кто не стучится — тому не открывают. Кто не пробует — у того не получается", null, 1, 170, 266, null, "Фёдор", 2, 0, 0, null },
                    { -30L, (short)31, 174007564L, "﻿Подлинным зеркалом нашего образа мыслей является наша жизнь", null, 1, 169, 148, null, "Кирилл", 1, 0, 0, null },
                    { -29L, (short)28, 174159235L, "﻿Перемены, происходящие в нашей жизни, есть следствие нашего выбора и наших решений", null, 1, 168, 1600, null, "Илья", 1, 0, 0, null },
                    { -28L, (short)16, 171830246L, "﻿Нужно не тратить время, а инвестировать в него", null, 1, 167, 3136, null, "Лукьянов Александр", 3, 0, 0, null },
                    { -27L, (short)16, 172182020L, "﻿Никто не изготовит замок без ключа, также и жизнь не даст проблемы без решения", null, 1, 166, 3074, null, "Иванов Илья", 2, 0, 0, null },
                    { -26L, (short)37, 171572505L, "﻿О нравственных качествах человека нужно судить не по отдельным его усилиям, а по его повседневной жизни", null, 1, 165, 1090, null, "Григорьев Гордей", 1, 0, 0, null },
                    { -25L, (short)24, 176023942L, "﻿Никогда не осуждайте человека, пока не пройдете долгий путь в его ботинках", null, 2, 164, 2576, null, "Зайцева Валерия", 2, 0, 0, null },
                    { -24L, (short)17, 174007564L, "﻿Если все сложилось не так, как вы ожидали, не расстраивайтесь. Божьи планы всегда лучше наших", null, 2, 163, 769, null, "Киселева Варвара", 1, 0, 0, null },
                    { -23L, (short)31, 174159235L, "﻿Музыканты рисуют свои картины на фоне тишины. Мы даем музыку, а вы даете тишину.", null, 2, 162, 1092, null, "Лукьянова Анастасия", 3, 0, 0, null },
                    { -22L, (short)21, 171830246L, "﻿Современная музыка хороша уже тем, что, если ты ошибешься, никто не заметит.", null, 2, 161, 1096, null, "Куликова Виктория", 2, 0, 0, null },
                    { -21L, (short)16, 171572505L, "Хороший инвестор — всё равно что прилежный студент. Каждый день я трачу часы на чтение финансовой прессы.", null, 2, 160, 642, null, "Макарова Валерия Макаровна", 1, 0, 0, null },
                    { -20L, (short)30, 171572505L, "﻿Мы не всегда свободны от ошибок, по поводу которых смеемся над другими.", null, 2, 159, 1156, null, "Троицкая Мария Артёмовна", 3, 0, 0, null },
                    { -19L, (short)25, 150993485L, "Я понял, что жизнь ничего не стоит, но я также понял, что ничто не стоит жизни.", null, 2, 158, 1058, null, "Михеева Аиша Михайловна", 2, 0, 0, null },
                    { -18L, (short)16, 206154094L, "﻿Жизнь измеряется не числом вдохов и выдохов, а числом мгновений, в которые перехватывает дыхание.", null, 2, 157, 1288, null, "Федорова Софья Егоровна", 1, 0, 0, null },
                    { -17L, (short)34, 179737040L, "﻿Стремитесь не к тому, чтобы добиться успеха, а к тому, чтобы твоя жизнь имела смысл", null, 2, 156, 200, null, "Вешнякова Валерия Яковлевна", 3, 0, 0, null },
                    { -16L, (short)17, 171572505L, "﻿Красота — это внешность, фото — искусство, а главное в жизни — доброе сердце, характер и чувства", null, 1, 155, 2192, null, "Степанов Валерий", 2, 0, 0, null },
                    { -15L, (short)33, 150993485L, "﻿Жизнь — не зебра из черных и белых полос, а шахматная доска. Здесь все зависит от твоего хода", null, 1, 154, 1184, null, "Кузнецов Серафим", 1, 0, 0, null },
                    { -14L, (short)25, 206154094L, "﻿Кто хочет жить для других, не должен пренебрегать собственной жизнью", null, 1, 153, 1057, null, "Медведев Иван", 1, 0, 0, null },
                    { -13L, (short)16, 179737040L, "﻿Живи так — чтобы люди, столкнувшись с тобой, улыбнулись, а, общаясь с тобой, стали чуточку счастливее", null, 1, 152, 672, null, "Мальцев Артём", 3, 0, 0, null },
                    { -12L, (short)36, 176023942L, "﻿Быстрее всего учишься в трех случаях — до 7 лет, на тренингах, и когда жизнь загнала тебя в угол", null, 1, 151, 193, null, "Иванов Ярослав Артёмович", 2, 0, 0, null },
                    { -11L, (short)19, 174007564L, "﻿Единственное правило в жизни, по которому нужно жить — оставаться человеком в любых ситуациях", null, 1, 150, 578, null, "Новиков Роман Андреевич", 1, 0, 0, null },
                    { -10L, (short)17, 174159235L, "﻿Чтобы вы ни делали, делайте это хорошо", null, 1, 149, 2688, null, "Рудаков Артём Артёмович", 3, 0, 0, null },
                    { -9L, (short)35, 171830246L, "﻿Когда мне тяжело, я всегда напоминаю себе о том, что если я сдамся — лучше не станет", null, 1, 148, 1160, null, "Трифонов Макар Янович", 2, 0, 0, null },
                    { -8L, (short)32, 206154094L, "﻿Удвоенное желание есть страсть, удвоенная страсть становится безумием.", null, 2, 147, 21, null, "Гончарова Ника Артёмовна", 3, 0, 0, null },
                    { -7L, (short)20, 179737040L, "﻿Характер подобен дереву, а репутация — его тени. Мы заботимся о тени, но на самом деле надо думать о дереве.", null, 2, 146, 194, null, "Исаева Полина Максимовна", 2, 0, 0, null },
                    { -6L, (short)17, 176023942L, "﻿Нельзя сдаваться не только после одного, но и после ста поражений.", null, 2, 145, 2562, null, "Костина Ксения Степановна", 1, 0, 0, null },
                    { -5L, (short)33, 172182020L, "﻿Можно всё время дурачить некоторых, можно некоторое время дурачить всех, но нельзя все время дурачить всех.", null, 2, 144, 1064, null, "Горшкова Варвара Антоновна", 1, 0, 0, null },
                    { -4L, (short)38, 174159235L, "﻿Грамм собственного опыта стоит дороже тонны чужих наставлений!", null, 1, 143, 2624, null, "Сафонов Иван Даниилович", 2, 0, 0, null },
                    { -3L, (short)27, 171830246L, "﻿Каждый день не может быть хорошим, важно лишь запомнить хорошее в каждом дне", null, 1, 142, 26, null, "Харитонов Александр Александрович", 1, 0, 0, null },
                    { -2L, (short)16, 174007564L, "﻿Окружай себя только теми людьми, кто будет тянуть тебя выше. Просто жизнь уже полна теми, кто хочет тянуть тебя вниз", null, 1, 141, 1296, null, "Фролов Евгений Дмитриевич", 3, 0, 0, null },
                    { -1L, (short)19, 172182020L, "﻿В конце жизни важны не прожитые годы, а то, как вы их прожили.", null, 2, 140, 2053, null, "Жданова Маргарита Юрьевна", 3, 0, 0, null }
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 171835519L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 174912365L);

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -65L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -64L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -63L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -62L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -61L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -60L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -59L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -58L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -57L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -56L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -55L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -54L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -53L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -52L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -51L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -50L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -49L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -48L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -47L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -46L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -45L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -44L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -43L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -42L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -41L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -40L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -39L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -38L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -37L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -36L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -35L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -34L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -33L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -32L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -31L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -30L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -29L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -28L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -27L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -26L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -25L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -24L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -23L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -22L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -21L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -20L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -19L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -18L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -17L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -16L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -15L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -14L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -13L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -12L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -11L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -10L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -9L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -8L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -7L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -6L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -5L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -4L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -3L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -2L });

            migrationBuilder.DeleteData(
                table: "UserMedia",
                keyColumns: new[] { "MediaFileId", "UserId" },
                keyValues: new object[] { "AgACAgIAAxkBAAITm2XohD-U_CkW9OQikaVWm3PH5RfMAAJZ1TEbQyZIS_JYw6dSEGvsAQADAgADeQADNAQ", -1L });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -65L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -64L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -63L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -62L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -61L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -60L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -59L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -58L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -57L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -56L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -55L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -54L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -53L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -52L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -51L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -50L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -49L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -48L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -47L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -46L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -45L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -44L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -43L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -42L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -41L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -40L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -39L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -38L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -37L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -36L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -35L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -34L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -33L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -32L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -31L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -30L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -29L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -28L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -27L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -26L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -25L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -24L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -23L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -22L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -21L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -20L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -19L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -18L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -17L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -16L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -15L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -14L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -13L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -12L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -11L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -10L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -9L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -8L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -7L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -6L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -5L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 150993485L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 171572505L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 171830246L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 172182020L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 174007564L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 174159235L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 176023942L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 179737040L);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 206154094L);
        }
    }
}
