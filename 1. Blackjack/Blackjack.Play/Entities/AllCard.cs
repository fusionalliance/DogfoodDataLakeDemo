using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Play.Entities
{
    public class AllCard
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsAce { get;  set; }
        public Suit Suit { get; set; }
    }
}