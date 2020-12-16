using DownloadAttachmentConsole.Model;
using LinqToDB.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DownloadAttachmentConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DataConnection.DefaultSettings = new SqlDbSettings();
            // star health integration
            StarHealthDownoadIntegration();



            List<string> ListOfURLs = new List<string>();
            int counter = 0;
            if (ListOfURLs.IsValid())
            {
                foreach (var item in ListOfURLs)
                {
                    downloadfile(item);
                    Console.WriteLine("File Downloaded :" + item + " | Counter :" + counter++);
                }
            }
            Console.ReadKey();

        }
        static void downloadfile(string url)
        {
            try
            {
                using (WebClient myWebClient = new WebClient())
                {

                    string folderName = "IHXPro-" + DateTime.Now.ToString("yyyy_MM_dd");
                    string folderPath = "D:\\DownloadFile\\" + folderName;
                    string FileName = folderPath + "\\" + "File_" + Guid.NewGuid().ToString();
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);
                    if (File.Exists(FileName))
                        FileName = FileName + "_" + Guid.NewGuid().ToString() + ".pdf";
                    else
                        FileName += ".pdf";
                    // Download the Web resource and save it into the current filesystem folder.
                    myWebClient.DownloadFile(url, FileName);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error :" + ex.Message);
            }
        }

        private static HttpClient CreateClient(string uri, Dictionary<string, string> headers = null, HttpClientHandler httpClientHandler = null)
        {
            HttpClient client = null;
            if (httpClientHandler != null)
            {
                client = new HttpClient(httpClientHandler)
                {
                    BaseAddress = new Uri(uri)
                };
            }
            else
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(uri)
                };
            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            if (headers != null && headers.Count > 0)
                headers.ToList().ForEach(x => client.DefaultRequestHeaders.Add(x.Key, x.Value));
            client.Timeout = TimeSpan.FromSeconds(10 * 60);
            return client;
        }

        public static V MakePostRestCall<K, V>(K request, String path, String serviceUri, Dictionary<string, string> Headers = null, HttpClientHandler httpClientHandler = null) where K : new()
        {
            V returnval = default;
            try
            {
                using (HttpClient restClient = CreateClient(serviceUri, Headers, httpClientHandler))
                {
                    var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                    //Thread.Sleep(60 * 1000);
                    HttpResponseMessage response = restClient.PostAsync(path, stringContent).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseAsString = response.Content.ReadAsStringAsync().Result;
                        returnval = JsonConvert.DeserializeObject<V>(responseAsString);
                    }
                    else
                    {
                        var responseAsString = response.Content.ReadAsStringAsync().Result;
                        throw new Exception((int)response.StatusCode + " $ " + responseAsString);
                    }
                }
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1);
                Console.WriteLine(e1.StackTrace);
            }

            return returnval;
        }


        #region Star Health Download Attachment
        public static void StarHealthDownoadIntegration()
        {

            Dictionary<string, string> DocPaths = new Dictionary<string, string>() {
                {"62838215","GAL_ReimbursementQueryLetter_CLMG_2021_231311_0352921_1608032950195.pdf"},
{"62725081","GAL_BillSummaryOtherProducts_CLMG_2021_211121_0549357_1607924052409.pdf"},
{"62739002","GAL_BillSummaryOtherProducts_CLMG_2021_161130_0549478_1607932657500.pdf"},
{"62728932","GAL_BillSummaryOtherProducts_CLMG_2021_211114_0549971_1607926684187.pdf"},
{"62745349","GAL_BillSummaryOtherProducts_CLMG_2021_171116_0565201_1607937077295.pdf"},
{"62726111","GAL_BillSummaryOtherProducts_CLMG_2021_171119_0565400_1607924829237.pdf"},
{"62730412","GAL_BillSummaryOtherProducts_CLMG_2021_170000_0572805_1607927565813.pdf"},
{"62746874","GAL_BillSummaryOtherProducts_CLMG_2021_170000_0587258_1607937935777.pdf"},
{"62725420","GAL_BillSummaryOtherProducts_CLMG_2021_151113_0439138_1607924322462.pdf"},
{"62740004","GAL_BillSummaryOtherProducts_CLMG_2021_131112_0533151_1607933394306.pdf"},
{"62737174","GAL_BillSummaryOtherProducts_CLMG_2021_211213_0553670_1607931458586.pdf"},
{"62739286","GAL_BillSummaryOtherProducts_CLMG_2021_141119_0473650_1607932858301.pdf"},
{"62751244","GAL_BillSummaryOtherProducts_CLMG_2021_111214_0473740_1607940237381.pdf"},
{"62739248","GAL_BillSummaryOtherProducts_CLMG_2021_161114_0516490_1607932841850.pdf"},
{"62735607","GAL_BillSummaryOtherProducts_CLMG_2021_161219_0534069_1607930523441.pdf"},
{"62735159","GAL_BillSummaryOtherProducts_CLMG_2021_181216_0538692_1607930287108.pdf"},
{"62735869","GAL_BillSummaryOtherProducts_CLMG_2021_151117_0575123_1607930660439.pdf"},
{"62736669","GAL_BillSummaryOtherProducts_CLMG_2021_131125_0589215_1607931145085.pdf"},
{"62738501","GAL_BillSummaryOtherProducts_CLMG_2021_141129_0603829_1607932332996.pdf"},
{"62866330","GAL_ReimbursementQueryLetter_CLMG_2021_181322_0368536_1608096031832.pdf"},
{"62747550","GAL_BillSummaryOtherProducts_CLMG_2021_211214_0459846_1607938291419.pdf"},
{"62739185","GAL_BillSummaryOtherProducts_CLMG_2021_161133_0493182_1607932790641.pdf"},
{"62738330","GAL_BillSummaryOtherProducts_CLMG_2021_211218_0493186_1607932239419.pdf"},
{"62743820","GAL_BillSummaryOtherProducts_CLMG_2021_131139_0535885_1607936179572.pdf"},
{"62735224","GAL_BillSummaryOtherProducts_CLMG_2021_151121_0600389_1607930311376.pdf"},
{"62748723","GAL_BillSummaryOtherProducts_CLMG_2021_151117_0393236_1607938876102.pdf"},
{"62739577","GAL_BillSummaryOtherProducts_CLMG_2021_161111_0480377_1607933057066.pdf"},
{"62743386","GAL_BillSummaryOtherProducts_CLMG_2021_221118_0489169_1607935925467.pdf"},
{"62742409","GAL_BillSummaryOtherProducts_CLMG_2021_161130_0531534_1607935264168.pdf"},
{"62749896","GAL_BillSummaryOtherProducts_CLMG_2021_161100_0544795_1607939486603.pdf"},
{"62725604","GAL_BillSummaryOtherProducts_CLMG_2021_161124_0545052_1607924497777.pdf"},
{"62741611","GAL_BillSummaryOtherProducts_CLMG_2021_211111_0549914_1607934684345.pdf"},
{"62724792","GAL_BillSummaryOtherProducts_CLMG_2021_171118_0556737_1607923830021.pdf"},
{"62726939","GAL_BillSummaryOtherProducts_CLMG_2021_181228_0581978_1607925498566.pdf"},
{"62744561","GAL_BillSummaryOtherProducts_CLMG_2021_161130_0603152_1607936603598.pdf"},
{"62736375","GAL_BillSummaryOtherProducts_CLMG_2021_151117_0608584_1607930936918.pdf"},
{"62740851","GAL_BillSummaryOtherProducts_CLMG_2021_161112_0476883_1607934087859.pdf"},
{"62749058","GAL_BillSummaryOtherProducts_CLMG_2021_181300_0539290_1607939050675.pdf"},
{"62726630","GAL_BillSummaryOtherProducts_CLMG_2021_161314_0553118_1607925247674.pdf"},
{"62730806","GAL_BillSummaryOtherProducts_CLMG_2021_181213_0596799_1607927823174.pdf"},
{"62725577","GAL_BillSummaryOtherProducts_CLMG_2021_110000_0597018_1607924306375.pdf"},
{"62737271","GAL_BillSummaryOtherProducts_CLMG_2021_161211_0433859_1607931539566.pdf"},
{"62749576","GAL_BillAssessmentSheetSCRC_CLMG_2021_181119_0472910_1607939311173.pdf"},
{"62736149","GAL_BillSummaryOtherProducts_CLMG_2021_211200_0524784_1607930829789.pdf"},
{"62750611","GAL_BillSummaryOtherProducts_CLMG_2021_161130_0533005_1607939887835.pdf"},
{"62740895","GAL_BillSummaryOtherProducts_CLMG_2021_211120_0574850_1607934114205.pdf"},
{"62743383","GAL_BillSummaryOtherProducts_CLMG_2021_131118_0603771_1607935923135.pdf"},
{"60860176","GAL_ReimbursementQueryLetter_CLMG_2021_181119_0442388_1605087773708.pdf"},
{"62888722","GAL_ReimbursementQueryLetter_CLMG_2021_181119_0442388_1608109521416.pdf"},
{"62867758","GAL_ReimbursementQueryLetter_CLMG_2021_171140_0516720_1608097003581.pdf"},
{"62740033","GAL_BillSummaryOtherProducts_CLMG_2021_161111_0518838_1607933423272.pdf"},
{"62855346","GAL_ReimbursementQueryLetter_CLMG_2021_170000_0513701_1608057740824.pdf"},
{"62730255","GAL_BillSummaryOtherProducts_CLMG_2021_191119_0525449_1607927445697.pdf"},
{"62744696","GAL_BillSummaryOtherProducts_CLMG_2021_131218_0535129_1607936701023.pdf"},
{"62736865","GAL_BillSummaryOtherProducts_CLMG_2021_161113_0546702_1607931264320.pdf"},
{"62746416","GAL_BillSummaryOtherProducts_CLMG_2021_181319_0551575_1607936937070.pdf"},
{"62867916","GAL_ReimbursementQueryLetter_CLMG_2021_171129_0553366_1608097102706.pdf"},
{"62728516","GAL_BillSummaryOtherProducts_CLMG_2021_151124_0569373_1607926432381.pdf"},
{"62727551","GAL_BillSummaryOtherProducts_CLMG_2021_161130_0576861_1607925870609.pdf"},
{"62727251","GAL_BillSummaryOtherProducts_CLMG_2021_171121_0600156_1607925683444.pdf"},
{"62741240","GAL_BillSummaryOtherProducts_CLMG_2021_161133_0497638_1607934398279.pdf"},
{"62739516","GAL_BillSummaryOtherProducts_CLMG_2021_131118_0544482_1607933010575.pdf"},
{"62739772","GAL_BillSummaryOtherProducts_CLMG_2021_191111_0474554_1607933225068.pdf"},
{"62738032","GAL_BillSummaryOtherProducts_CLMG_2021_181311_0529790_1607932028360.pdf"},
{"62728362","GAL_BillSummaryOtherProducts_CLMG_2021_211226_0530682_1607926356854.pdf"},
{"62887377","GAL_ReimbursementQueryLetter_CLMG_2021_181215_0542114_1608108853674.pdf"},
{"62736541","GAL_BillSummaryOtherProducts_CLMG_2021_181113_0544122_1607931077021.pdf"},
{"62747688","GAL_BillSummaryOtherProducts_CLMG_2021_161132_0603527_1607938378286.pdf"},
{"62735140","GAL_BillSummaryOtherProducts_CLMG_2021_161118_0500341_1607930258068.pdf"},
{"62751258","GAL_BillSummaryOtherProducts_CLMG_2021_151128_0483086_1607940252252.pdf"},
{"62743957","GAL_BillSummaryOtherProducts_CLMG_2021_151119_0499014_1607936265549.pdf"},
{"62745896","GAL_BillSummaryOtherProducts_CLMG_2021_221113_0508138_1607937376175.pdf"},
{"62867647","GAL_ReimbursementQueryLetter_CLMG_2021_171148_0508218_1608096943078.pdf"},
{"62725943","GAL_BillSummaryOtherProducts_CLMG_2021_211121_0524611_1607924697447.pdf"},
{"62751318","GAL_BillSummaryOtherProducts_CLMG_2021_181216_0552708_1607940286835.pdf"},
{"62726829","GAL_BillSummaryOtherProducts_CLMG_2021_141113_0597517_1607925413444.pdf"},
{"62748089","GAL_BillSummaryOtherProducts_CLMG_2021_181219_0569086_1607938570652.pdf"},
{"62746786","GAL_BillSummaryOtherProducts_CLMG_2021_181216_0584272_1607937889929.pdf"},
{"62726120","GAL_BillSummaryOtherProducts_CLMG_2021_151118_0430644_1607924813394.pdf"},
{"62743543","GAL_BillSummaryOtherProducts_CLMG_2021_161215_0524099_1607935990429.pdf"},
{"62728687","GAL_BillSummaryOtherProducts_CLMG_2021_161111_0523968_1607926540006.pdf"},
{"62852715","GAL_PreauthWithDrawLetter_NonGMC_CLMG_2021_211111_0531804_1608045711510.pdf"},
{"62873672","GAL_ReimbursementQueryLetter_CLMG_2021_111118_0449108_1608100775415.pdf"},
{"62728249","GAL_BillSummaryOtherProducts_CLMG_2021_211218_0481149_1607926291600.pdf"},
{"62738668","GAL_BillSummaryOtherProducts_CLMG_2021_131134_0536093_1607932459360.pdf"},
{"62725606","GAL_BillSummaryOtherProducts_CLMG_2021_151100_0606147_1607924498095.pdf"},
{"62747113","GAL_BillSummaryOtherProducts_CLMG_2021_151115_0295118_1607938026078.pdf"},
{"62853678","GAL_ReimbursementQueryLetter_CLMG_2021_231123_0513159_1608048977621.pdf"},
{"62890357","GAL_ReimbursementQueryLetter_CLMG_2021_181213_0378956_1608110329979.pdf"},
{"62737267","GAL_BillSummaryOtherProducts_CLMG_2021_191122_0399226_1607931514212.pdf"},
{"62742977","GAL_BillSummaryOtherProducts_CLMG_2021_221114_0514427_1607935659995.pdf"},
{"62741297","GAL_BillSummaryOtherProducts_CLMG_2021_161130_0514827_1607934484165.pdf"},
{"62726276","GAL_BillSummaryOtherProducts_CLMG_2021_181119_0555763_1607924973284.pdf"},
{"62751277","GAL_BillSummaryOtherProducts_CLMG_2021_211121_0565281_1607940272516.pdf"},
{"62737139","GAL_BillSummaryOtherProducts_CLMG_2021_131128_0580077_1607931438412.pdf"},
{"58716746","GAL_CashlessEnhancementApprovalLetter_CLMG_2021_181100_0391270_1601986542452.pdf"},
{"62912515","GAL_ReimbursementQueryLetter_CLMG_2021_181114_0539555_1608120995343.pdf"},
{"62727643","GAL_BillSummaryOtherProducts_CLMG_2021_151121_0561484_1607925943526.pdf"},
{"62749096","GAL_BillSummaryOtherProducts_CLMG_2021_211100_0576063_1607939000449.pdf"},
{"62728213","GAL_BillSummaryOtherProducts_CLMG_2021_151113_0576332_1607926266584.pdf"},
{"62742229","GAL_BillSummaryOtherProducts_CLMG_2021_201114_0596947_1607935113938.pdf"},
{"62725170","GAL_BillSummaryOtherProducts_CLMG_2021_181100_0448432_1607924117263.pdf"},
{"62749126","GAL_BillSummaryOtherProducts_CLMG_2021_171147_0535987_1607939094452.pdf"},
{"62739829","GAL_BillSummaryOtherProducts_CLMG_2021_151118_0545798_1607933257962.pdf"},
{"62739220","GAL_BillSummaryOtherProducts_CLMG_2021_161219_0603561_1607932824237.pdf"},
{"62855430","GAL_ReimbursementQueryLetter_CLMG_2021_171213_0338733_1608058450670.pdf"},
{"62751352","GAL_BillSummaryOtherProducts_CLMG_2021_110000_0427031_1607940294272.pdf"},
{"62748613","GAL_BillSummaryOtherProducts_CLMG_2021_141142_0477338_1607938828099.pdf"},
{"62744472","GAL_BillSummaryOtherProducts_CLMG_2021_161130_0503282_1607936582832.pdf"},
{"62727945","GAL_BillSummaryOtherProducts_CLMG_2021_181119_0518912_1607926119135.pdf"},
{"62726608","GAL_BillSummaryOtherProducts_CLMG_2021_181136_0540003_1607925188187.pdf"},
{"62750407","GAL_BillSummaryOtherProducts_CLMG_2021_211117_0554211_1607939782203.pdf"},
{"62743730","GAL_BillSummaryOtherProducts_CLMG_2021_151114_0554327_1607936121171.pdf"},
{"62745664","GAL_BillSummaryOtherProducts_CLMG_2021_201123_0577336_1607937250090.pdf"},
{"62741281","GAL_BillSummaryOtherProducts_CLMG_2021_131128_0592242_1607934462461.pdf"},
{"62736951","GAL_BillSummaryOtherProducts_CLMG_2021_191125_0430778_1607931314532.pdf"},
{"62747178","GAL_BillSummaryOtherProducts_CLMG_2021_161131_0488986_1607938106028.pdf"},
{"62748756","GAL_BillSummaryOtherProducts_CLMG_2021_161124_0505464_1607938892229.pdf"},
{"62750212","GAL_BillSummaryOtherProducts_CLMG_2021_181112_0563602_1607939658278.pdf"},
{"62744471","GAL_BillSummaryOtherProducts_CLMG_2021_110000_0574015_1607936554925.pdf"},
{"62741220","GAL_BillSummaryOtherProducts_CLMG_2021_211111_0616469_1607934397941.pdf"},
{"62727085","GAL_BillSummaryOtherProducts_CLMG_2021_181127_0501421_1607925584687.pdf"},
{"61749292","GAL_ReimbursementQueryLetter_CLMG_2021_700002_0513892_1606470274127.pdf"},
{"62890216","GAL_ReimbursementQueryLetter_CLMG_2021_700002_0513892_1608110272012.pdf"},
{"62748199","GAL_BillSummaryOtherProducts_CLMG_2021_221120_0514567_1607938637002.pdf"},
{"62739882","GAL_BillSummaryOtherProducts_CLMG_2021_170000_0527254_1607933249885.pdf"},
{"62749436","GAL_BillSummaryOtherProducts_CLMG_2021_211200_0531495_1607939251446.pdf"},
{"62744686","GAL_BillSummaryOtherProducts_CLMG_2021_181123_0546458_1607936696174.pdf"},
{"62730373","GAL_BillSummaryOtherProducts_CLMG_2021_181200_0549934_1607927590111.pdf"},
{"62739679","GAL_BillSummaryOtherProducts_CLMG_2021_201119_0564808_1607933143902.pdf"},
{"62726345","GAL_BillSummaryOtherProducts_CLMG_2021_171119_0586987_1607925029831.pdf"},
{"62729157","GAL_BillSummaryOtherProducts_CLMG_2021_151121_0603003_1607926821396.pdf"},
{"61831751","GAL_BillSummaryOtherProducts_CLMG_2021_170000_0217811_1606564820162.pdf"},
{"62740336","GAL_BillSummaryOtherProducts_CLMG_2021_161312_0517941_1607933645351.pdf"},
{"62737635","GAL_BillSummaryOtherProducts_CLMG_2021_161118_0525967_1607931766088.pdf"},
{"62749497","GAL_BillSummaryOtherProducts_CLMG_2021_161111_0553539_1607939281871.pdf"},
{"62899744","GAL_ReimbursementQueryLetter_CLMG_2021_121411_0473435_1608114750059.pdf"},
{"62745558","GAL_BillSummaryOtherProducts_CLMG_2021_141129_0491108_1607937193772.pdf"},
{"62889695","GAL_ReimbursementQueryLetter_CLMG_2021_700004_0500154_1608110031500.pdf"},
{"62739967","GAL_BillSummaryOtherProducts_CLMG_2021_211100_0507953_1607933356891.pdf"},
{"62726060","GAL_BillSummaryOtherProducts_CLMG_2021_141134_0552240_1607924779744.pdf"},
{"62725232","GAL_BillSummaryOtherProducts_CLMG_2021_111113_0581875_1607924120313.pdf"},
{"62730677","GAL_BillSummaryOtherProducts_CLMG_2021_151119_0526959_1607927739485.pdf"},
{"62746197","GAL_BillSummaryOtherProducts_CLMG_2021_170000_0547276_1607937484676.pdf"},
{"62738842","GAL_BillSummaryOtherProducts_CLMG_2021_110000_0584637_1607932548885.pdf"},
{"62728988","GAL_BillSummaryOtherProducts_CLMG_2021_211212_0410755_1607926722275.pdf"},
{"62744383","GAL_BillSummaryOtherProducts_CLMG_2021_161131_0472709_1607936523512.pdf"},
{"62750149","GAL_BillSummaryOtherProducts_CLMG_2021_211120_0551475_1607939622947.pdf"},
{"62750705","GAL_BillSummaryOtherProducts_CLMG_2021_181200_0552002_1607939943435.pdf"},
{"62737693","GAL_BillSummaryOtherProducts_CLMG_2021_181123_0567047_1607931811635.pdf"},
{"62750518","GAL_BillSummaryOtherProducts_CLMG_2021_151123_0581092_1607939841185.pdf"},
{"62749301","GAL_BillSummaryOtherProducts_CLMG_2021_151118_0594416_1607939166943.pdf"},
{"62739258","GAL_BillSummaryOtherProducts_CLMG_2021_151118_0599952_1607932800931.pdf"},
{"62745037","GAL_BillSummaryOtherProducts_CLMG_2021_211200_0470837_1607936905710.pdf"},
{"62866799","GAL_ReimbursementQueryLetter_CLMG_2021_171147_0504755_1608096357201.pdf"},
{"62745373","GAL_BillSummaryOtherProducts_CLMG_2021_161111_0528583_1607937070052.pdf"},
{"62729162","GAL_BillSummaryOtherProducts_CLMG_2021_171212_0550908_1607926844027.pdf"},
{"62744274","GAL_BillSummaryOtherProducts_CLMG_2021_181218_0572010_1607936472802.pdf"},
{"62738278","GAL_BillSummaryOtherProducts_CLMG_2021_131140_0617404_1607932205455.pdf"},
{"62742781","GAL_BillSummaryOtherProducts_CLMG_2021_161114_0502646_1607935504608.pdf"},
{"62739097","GAL_BillSummaryOtherProducts_CLMG_2021_211111_0521769_1607932713099.pdf"},
{"62737673","GAL_BillSummaryOtherProducts_CLMG_2021_211128_0513395_1607931769594.pdf"},
{"62725004","GAL_BillSummaryOtherProducts_CLMG_2021_171118_0584010_1607924001286.pdf"},
{"62745271","GAL_BillSummaryOtherProducts_CLMG_2021_131137_0598964_1607937035521.pdf"},
{"62907114","GAL_ReimbursementQueryLetter_CLMG_2021_121126_0511713_1608118145375.pdf"},
{"62747418","GAL_BillSummaryOtherProducts_CLMG_2021_181319_0577444_1607938221977.pdf"},
{"62729495","GAL_BillSummaryOtherProducts_CLMG_2021_151117_0612686_1607927089632.pdf"},
{"62749845","GAL_BillSummaryOtherProducts_CLMG_2021_211211_0551168_1607939460014.pdf"},
{"62737852","GAL_BillSummaryOtherProducts_CLMG_2021_170000_0580869_1607931923420.pdf"},
{"62729206","GAL_BillSummaryOtherProducts_CLMG_2021_211213_0528278_1607926877331.pdf"},
{"62735609","GAL_BillSummaryOtherProducts_CLMG_2021_130000_0536414_1607930518189.pdf"},
{"62750111","GAL_BillSummaryOtherProducts_CLMG_2021_171128_0547483_1607939602429.pdf"},
{"62742797","GAL_BillSummaryOtherProducts_CLMG_2021_201126_0547505_1607935512589.pdf"},
{"62750982","GAL_BillSummaryOtherProducts_CLMG_2021_211218_0558875_1607940118977.pdf"},
{"62750764","GAL_BillSummaryOtherProducts_CLMG_2021_211215_0554687_1607939973759.pdf"},
{"62739060","GAL_BillSummaryOtherProducts_CLMG_2021_191212_0487820_1607932684534.pdf"},
{"62863300","GAL_ReimbursementQueryLetter_CLMG_2021_111113_0502398_1608092894952.pdf"},
{"62745786","GAL_BillSummaryOtherProducts_CLMG_2021_161212_0520521_1607937317238.pdf"},
{"62748626","GAL_BillSummaryOtherProducts_CLMG_2021_221113_0550934_1607938828457.pdf"},
{"62742274","GAL_BillSummaryOtherProducts_CLMG_2021_700016_0447271_1607935136066.pdf"},
{"62750744","GAL_BillSummaryOtherProducts_CLMG_2021_171132_0479049_1607939969137.pdf"},
{"62746094","GAL_BillSummaryOtherProducts_CLMG_2021_151119_0495771_1607937435214.pdf"},
{"62729504","GAL_BillSummaryOtherProducts_CLMG_2021_181115_0522102_1607927103986.pdf"},
{"62726501","GAL_BillSummaryOtherProducts_CLMG_2021_181211_0529961_1607925149453.pdf"},
{"62729062","GAL_BillSummaryOtherProducts_CLMG_2021_191200_0530219_1607926688882.pdf"},
{"62744570","GAL_BillSummaryOtherProducts_CLMG_2021_211112_0549290_1607936616625.pdf"},
{"62735839","GAL_BillSummaryOtherProducts_CLMG_2021_181218_0556441_1607930680401.pdf"},
{"62730314","GAL_BillSummaryOtherProducts_CLMG_2021_131116_0558359_1607927553084.pdf"},
{"62736896","GAL_BillSummaryOtherProducts_CLMG_2021_201126_0564690_1607931288437.pdf"},
{"62869135","GAL_ReimbursementQueryLetter_CLMG_2021_181225_0564162_1608097884803.pdf"},
{"62749461","GAL_BillSummaryOtherProducts_CLMG_2021_171117_0601657_1607939263550.pdf"},
{"62737334","GAL_BillSummaryOtherProducts_CLMG_2021_161100_0530469_1607931575910.pdf"},
{"62730414","GAL_BillSummaryOtherProducts_CLMG_2021_211124_0536541_1607927601896.pdf"},
{"62874911","GAL_ReimbursementQueryLetter_CLMG_2021_111113_0539925_1608101482021.pdf"},
{"62749965","GAL_BillSummaryOtherProducts_CLMG_2021_171143_0593025_1607939505821.pdf"},
{"62729329","GAL_BillSummaryOtherProducts_CLMG_2021_151114_0599873_1607926947542.pdf"},
{"62878588","GAL_ReimbursementQueryLetter_CLMG_2021_221116_0504897_1608103671700.pdf"},
{"62876926","GAL_ReimbursementQueryLetter_CLMG_2021_191123_0550462_1608102662466.pdf"},
{"62744123","GAL_BillSummaryOtherProducts_CLMG_2021_211114_0470144_1607936369875.pdf"},
{"62747672","GAL_BillSummaryOtherProducts_CLMG_2021_151118_0530083_1607938338253.pdf"},
{"62740686","GAL_BillSummaryOtherProducts_CLMG_2021_211111_0541911_1607933954234.pdf"},
{"62736798","GAL_BillSummaryOtherProducts_CLMG_2021_211216_0506406_1607931231780.pdf"},
{"62747794","GAL_BillSummaryOtherProducts_CLMG_2021_211121_0514176_1607938435829.pdf"},
{"62746058","GAL_BillSummaryOtherProducts_CLMG_2021_110000_0565283_1607937499194.pdf"},
{"62737153","GAL_BillSummaryOtherProducts_CLMG_2021_181217_0572888_1607931448285.pdf"},
{"62745784","GAL_BillSummaryOtherProducts_CLMG_2021_161130_0579717_1607937318952.pdf"},
{"62748290","GAL_BillSummaryOtherProducts_CLMG_2021_151130_0383908_1607938667427.pdf"},
{"62750529","GAL_BillSummaryOtherProducts_CLMG_2021_171114_0539296_1607939846350.pdf"},
{"62877097","GAL_ReimbursementQueryLetter_CLMG_2021_181119_0552647_1608102759536.pdf"},
{"62730841","GAL_BillSummaryOtherProducts_CLMG_2021_161219_0553727_1607927845271.pdf"},
{"62750007","GAL_BillSummaryOtherProducts_CLMG_2021_181123_0568600_1607939538708.pdf"},
{"62747159","GAL_BillSummaryOtherProducts_CLMG_2021_181216_0582753_1607938097225.pdf"},
{"62735473","GAL_BillSummaryOtherProducts_CLMG_2021_151113_0596495_1607930444344.pdf"},
{"62728877","GAL_BillSummaryOtherProducts_CLMG_2021_151118_0615033_1607926631425.pdf"},
{"62750512","GAL_BillSummaryOtherProducts_CLMG_2021_181118_0561025_1607939836398.pdf"},
{"62869479","GAL_ReimbursementQueryLetter_CLMG_2021_181118_0567324_1608098156289.pdf"},
{"62749127","GAL_BillSummaryOtherProducts_CLMG_2021_211111_0546871_1607939093080.pdf"},
{"62726954","GAL_BillSummaryOtherProducts_CLMG_2021_151115_0562516_1607925459413.pdf"},
{"62726865","GAL_BillSummaryOtherProducts_CLMG_2021_171121_0591975_1607925423483.pdf"},
{"62744196","GAL_BillSummaryOtherProducts_CLMG_2021_151119_0606874_1607936410608.pdf"},
{"62890559","GAL_ReimbursementQueryLetter_CLMG_2021_700004_0524427_1608110414524.pdf"},
{"62745621","GAL_BillSummaryOtherProducts_CLMG_2021_161130_0594957_1607937187620.pdf"},
{"62737169","GAL_BillSummaryOtherProducts_CLMG_2021_161123_0491721_1607931457465.pdf"},
{"62729179","GAL_BillSummaryOtherProducts_CLMG_2021_111100_0562892_1607926853829.pdf"},
{"62745933","GAL_BillAssessmentSheetSCRC_CLMG_2021_181316_0570342_1607937415534.pdf"},
{"62736291","GAL_BillSummaryOtherProducts_CLMG_2021_131128_0599049_1607930915412.pdf"},
{"62745233","GAL_BillSummaryOtherProducts_CLMG_2021_151120_0606206_1607937002390.pdf"},
{"62740997","GAL_BillSummaryOtherProducts_CLMG_2021_161217_0541547_1607934212237.pdf"},
{"62738370","GAL_BillSummaryOtherProducts_CLMG_2021_171135_0573273_1607932262052.pdf"},
{"62739110","GAL_BillSummaryOtherProducts_CLMG_2021_121116_0446835_1607932735181.pdf"},
{"62724926","GAL_BillSummaryOtherProducts_CLMG_2021_181123_0437235_1607923937834.pdf"},
{"62727430","GAL_BillSummaryOtherProducts_CLMG_2021_181218_0503844_1607925794928.pdf"},
{"62743672","GAL_BillSummaryOtherProducts_CLMG_2021_161125_0512536_1607936097144.pdf"},
{"62737680","GAL_BillSummaryOtherProducts_CLMG_2021_121125_0512663_1607931798677.pdf"},
{"62741942","GAL_BillSummaryOtherProducts_CLMG_2021_160000_0542119_1607934944564.pdf"},
{"62738692","GAL_BillSummaryOtherProducts_CLMG_2021_151118_0548666_1607932467478.pdf"},
{"62739536","GAL_BillSummaryOtherProducts_CLMG_2021_211111_0601242_1607933023346.pdf"},
{"62746904","GAL_BillSummaryOtherProducts_CLMG_2021_151118_0377919_1607937954596.pdf"},
{"62726628","GAL_BillSummaryOtherProducts_CLMG_2021_211122_0549676_1607925246457.pdf"},
{"62750368","GAL_BillSummaryOtherProducts_CLMG_2021_161114_0552212_1607939761453.pdf"},
{"62727951","GAL_BillSummaryOtherProducts_CLMG_2021_211119_0501276_1607926117012.pdf"},
{"62739210","GAL_BillSummaryOtherProducts_CLMG_2021_171132_0509068_1607932819195.pdf"},
{"62740377","GAL_BillSummaryOtherProducts_CLMG_2021_161118_0517849_1607933711888.pdf"},
{"62736861","GAL_BillSummaryOtherProducts_CLMG_2021_121123_0538963_1607931265307.pdf"},
{"62745419","GAL_BillSummaryOtherProducts_CLMG_2021_211118_0508056_1607937122296.pdf"},
{"62746454","GAL_BillSummaryOtherProducts_CLMG_2021_211222_0524700_1607937721963.pdf"},
{"62741197","GAL_BillSummaryOtherProducts_CLMG_2021_191200_0524992_1607934371976.pdf"},
{"62736726","GAL_BillSummaryOtherProducts_CLMG_2021_151118_0552472_1607931187376.pdf"},
{"62750821","GAL_BillSummaryOtherProducts_CLMG_2021_181125_0555900_1607940016538.pdf"},
{"62745018","GAL_BillSummaryOtherProducts_CLMG_2021_170000_0568006_1607936849672.pdf"},
{"62747571","GAL_BillSummaryOtherProducts_CLMG_2021_171116_0586370_1607938294197.pdf"},
{"62742921","GAL_BillSummaryOtherProducts_CLMG_2021_151119_0614431_1607935591674.pdf"},
{"62740391","GAL_BillSummaryOtherProducts_CLMG_2021_201124_0443808_1607933724605.pdf"},
{"62737718","GAL_BillAssessmentSheetSCRC_CLMG_2021_111128_0469228_1607931831952.pdf"},
{"62743503","GAL_BillSummaryOtherProducts_CLMG_2021_170000_0511551_1607935984241.pdf"},
{"62735195","GAL_BillSummaryOtherProducts_CLMG_2021_161213_0518445_1607930293193.pdf"},
{"62743001","GAL_BillSummaryOtherProducts_CLMG_2021_171130_0534782_1607935660320.pdf"},
{"62736594","GAL_BillSummaryOtherProducts_CLMG_2021_211124_0535348_1607931092118.pdf"},
{"62749964","GAL_BillSummaryOtherProducts_CLMG_2021_131115_0577455_1607939515029.pdf"},
{"62729139","GAL_BillSummaryOtherProducts_CLMG_2021_170000_0592183_1607926767605.pdf"},
{"62742313","GAL_BillSummaryOtherProducts_CLMG_2021_161134_0531663_1607935191257.pdf"},
{"62749533","GAL_BillSummaryOtherProducts_CLMG_2021_211211_0551142_1607939307253.pdf"},
{"62750770","GAL_BillSummaryOtherProducts_CLMG_2021_171122_0474768_1607939986697.pdf"},
{"62744878","GAL_BillSummaryOtherProducts_CLMG_2021_181121_0475322_1607936819308.pdf"},
{"62739599","GAL_BillSummaryOtherProducts_CLMG_2021_141129_0547950_1607932919814.pdf"},
{"62744773","GAL_BillSummaryOtherProducts_CLMG_2021_161118_0563150_1607936759837.pdf"},
{"62885490","GAL_ReimbursementQueryLetter_CLMG_2021_191112_0504440_1608107994842.pdf"},
{"62735490","GAL_BillSummaryOtherProducts_CLMG_2021_161134_0537228_1607930464694.pdf"},
{"62867463","GAL_ReimbursementQueryLetter_CLMG_2021_181119_0558696_1608096829876.pdf"},
{"62737152","GAL_BillSummaryOtherProducts_CLMG_2021_151112_0610196_1607931428130.pdf"},
{"62735107","GAL_BillSummaryOtherProducts_CLMG_2021_121111_0375047_1607930251528.pdf"},
{"62746062","GAL_BillSummaryOtherProducts_CLMG_2021_161111_0586189_1607937498089.pdf"},
{"62740258","GAL_BillSummaryOtherProducts_CLMG_2021_171126_0574099_1607933601429.pdf"},
{"62742421","GAL_BillSummaryOtherProducts_CLMG_2021_211218_0483520_1607935255948.pdf"},
{"62738646","GAL_BillSummaryOtherProducts_CLMG_2021_700013_0548208_1607932448744.pdf"},
{"62737684","GAL_BillSummaryOtherProducts_CLMG_2021_181227_0563923_1607931807920.pdf"},
{"62750267","GAL_BillSummaryOtherProducts_CLMG_2021_131300_0585211_1607939669980.pdf"},
{"62727490","GAL_BillSummaryOtherProducts_CLMG_2021_151118_0599351_1607925822571.pdf"},
{"62888327","GAL_ReimbursementQueryLetter_CLMG_2021_181112_0503289_1608109291608.pdf"},
{"62747240","GAL_BillSummaryOtherProducts_CLMG_2021_181111_0503452_1607938148366.pdf"},
{"62728509","GAL_BillSummaryOtherProducts_CLMG_2021_221122_0503706_1607926437201.pdf"},
{"62747719","GAL_BillSummaryOtherProducts_CLMG_2021_181315_0529891_1607938390349.pdf"},
{"62178860","GAL_BillSummaryOtherProducts_CLMG_2021_171130_0542213_1607012984318.pdf"},
{"62742257","GAL_BillSummaryOtherProducts_CLMG_2021_171130_0542213_1607935140295.pdf"},
{"62741049","GAL_BillSummaryOtherProducts_CLMG_2021_151112_0601118_1607934236028.pdf"},
{"62748034","GAL_BillSummaryOtherProducts_CLMG_2021_151119_0489692_1607938520628.pdf"},
{"62730728","GAL_BillSummaryOtherProducts_CLMG_2021_181112_0557715_1607927776838.pdf"},
{"62747965","GAL_BillSummaryOtherProducts_CLMG_2021_161111_0572466_1607938518146.pdf"},
{"62749425","GAL_BillSummaryOtherProducts_CLMG_2021_161130_0467114_1607939241840.pdf"},
{"62750197","GAL_BillSummaryOtherProducts_CLMG_2021_211114_0546031_1607939605082.pdf"},
{"62868051","GAL_ReimbursementQueryLetter_CLMG_2021_171148_0561315_1608097184809.pdf"},
{"62746235","GAL_BillSummaryOtherProducts_CLMG_2021_181200_0575787_1607937603158.pdf"},
{"62729382","GAL_BillSummaryOtherProducts_CLMG_2021_161200_0596907_1607926766695.pdf"},
{"62738456","GAL_BillSummaryOtherProducts_CLMG_2021_170000_0475088_1607932314781.pdf"},
{"62750980","GAL_BillSummaryOtherProducts_CLMG_2021_161115_0524822_1607940118341.pdf"},
{"62735088","GAL_BillSummaryOtherProducts_CLMG_2021_211125_0574835_1607930232578.pdf"},
{"62740441","GAL_BillSummaryOtherProducts_CLMG_2021_151113_0589479_1607933767797.pdf"},
{"62902511","GAL_ReimbursementQueryLetter_CLMG_2021_111127_0426230_1608115994597.pdf"},
{"62728379","GAL_BillSummaryOtherProducts_CLMG_2021_181125_0469234_1607926368048.pdf"},
{"62749529","GAL_BillSummaryOtherProducts_CLMG_2021_211200_0511620_1607939299516.pdf"},
{"62751100","GAL_BillAssessmentSheetSCRC_CLMG_2021_181134_0556992_1607940178623.pdf"},
{"62745214","GAL_BillSummaryOtherProducts_CLMG_2021_121215_0562448_1607937004508.pdf"},
{"62748225","GAL_BillSummaryOtherProducts_CLMG_2021_211113_0569430_1607938639412.pdf"},
{"62750960","GAL_BillSummaryOtherProducts_CLMG_2021_211218_0569979_1607940102450.pdf"},
{"62738402","GAL_BillSummaryOtherProducts_CLMG_2021_151114_0606938_1607932263125.pdf"},
{"62743638","GAL_BillSummaryOtherProducts_CLMG_2021_161118_0497712_1607936061006.pdf"},
{"62728661","GAL_BillSummaryOtherProducts_CLMG_2021_181227_0548572_1607926534412.pdf"},
{"62735474","GAL_BillSummaryOtherProducts_CLMG_2021_181117_0559908_1607930459047.pdf"},
{"62728901","GAL_BillSummaryOtherProducts_CLMG_2021_161120_0501594_1607926670226.pdf"},
{"62737226","GAL_BillSummaryOtherProducts_CLMG_2021_161112_0548394_1607931500901.pdf"},
{"62734929","GAL_BillSummaryOtherProducts_CLMG_2021_150000_0606146_1607930077464.pdf"},
{"62735806","GAL_BillSummaryOtherProducts_CLMG_2021_161132_0364496_1607930649798.pdf"},
{"62738179","GAL_BillSummaryOtherProducts_CLMG_2021_181219_0566074_1607932137186.pdf"},
{"62741392","GAL_BillSummaryOtherProducts_CLMG_2021_161113_0566314_1607934567904.pdf"},
{"62745327","GAL_BillSummaryOtherProducts_CLMG_2021_151115_0374957_1607937067936.pdf"},
{"62890876","GAL_ReimbursementQueryLetter_CLMG_2021_700001_0529142_1608110547499.pdf"},
{"62876406","GAL_ReimbursementQueryLetter_CLMG_2021_181315_0530125_1608102355318.pdf"},
{"62748657","GAL_BillSummaryOtherProducts_CLMG_2021_181315_0555907_1607938841994.pdf"},
{"62748981","GAL_BillSummaryOtherProducts_CLMG_2021_181315_0563945_1607939010666.pdf"},
{"62727746","GAL_BillSummaryOtherProducts_CLMG_2021_151116_0578250_1607925987989.pdf"},
{"62744735","GAL_BillSummaryOtherProducts_CLMG_2021_700002_0593433_1607936721386.pdf"}
};
            DocPaths.ToList().ForEach(x =>
            {
                try
                {
                    SatrHealthUploadAttachment.UploadAttachments(x.Key, x.Value);
                    Console.WriteLine("DateTime:" + DateTime.Now.ToString() + "Document Uploaded | Key " + x.Key + " | Value" + x.Value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DateTime:" + DateTime.Now.ToString() + "Excetion : " + ex.Message);
                }
            });

        }
        #endregion

    }
}
