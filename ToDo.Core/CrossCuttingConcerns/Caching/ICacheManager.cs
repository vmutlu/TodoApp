namespace ToDo.Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key); // cache getir
        object Get(string key);// cache getir
        void Add(string key,object value,int duration); // cache ekle
        bool IsAdd(string key); // cache varmı 
        void Remove(string key); //cache sil
        void RemoveByPattern(string pattern); // cache de verilen degerleri sil. e.g. Get ile başlayanları cacheden sil
    }
}
