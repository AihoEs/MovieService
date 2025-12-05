using MoviesOnline.Interfaces;

namespace MoviesOnline.Services.UserServices
{
    public class IdGenerator : IIDGenerate
    {
        public int IdGenerate() 
        {
            int[] FullId = new int[7];
            Random rnd = new Random();
            for(int i = 0; i < FullId.Length; i++)
            {
                FullId[i] = rnd.Next(0,9);
            }
            int sliceId = int.Parse(string.Join("",FullId));
            return sliceId;
        }
    }
}
