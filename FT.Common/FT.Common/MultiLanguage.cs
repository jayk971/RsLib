using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;
namespace RsLib.Common
{
    public class Lang
    {
        // 加入資源檔 LangPack.zh-TW.resx
        // recource base name = NoSew.Recipe.LangPack
        // 設定名稱 跟 值
        // Get text (設定名稱) 回傳對應值
        static ResourceManager resourceMar;
        public  Lang(string resourceBaseName)
        {
            resourceMar = new ResourceManager(resourceBaseName, Assembly.GetExecutingAssembly());

        }

        public static string GetText(string Key)
        {
            return resourceMar.GetString(Key);
        }

        public static void SetUILang(LangCode lang)
        {
            CultureInfo CI = new CultureInfo(TranLangCode(lang));
            Thread.CurrentThread.CurrentUICulture = CI;
        }

        private static string TranLangCode(LangCode Input)
        {
            switch (Input)
            {
                case LangCode.en:
                    return "en";
                case LangCode.zh_TW:
                    return "zh-TW";
                case LangCode.zh_CN:
                    return "zh-CN";
                case LangCode.vi:
                    return "vi";
                default:
                    return "en";
            }
        }
    }
    public enum LangCode : int
    {
        en = 0,
        zh_TW,
        zh_CN,
        vi
    }

}