using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Framework;

public struct Loot
{
    #region Field/Properties

    /// <summary>
    /// Drop rate of items
    /// </summary>
    public int Rate;

    /// <summary>
    /// Range between min-max gold
    /// </summary>
    public Range? Money;

    /// <summary>
    /// Range between min-max coins
    /// </summary>
    public Range? Coin;

    /// <summary>
    /// List of items
    /// </summary>
    public List<string> Items;

    public readonly bool Nothing => Money == null &&
                                    Coin == null &&
                                    Items == null;
    #endregion


    #region Helper methods

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append(string.Format("Rate: {0}", this.Rate.ToString()));

        if (this.Money != null)
        {
            sb.Append(string.Format(", Money: [{0}]", this.Money.ToString()));
        }
        if (this.Coin != null)
        {
            sb.Append(string.Format(", Coin: [{0}]", this.Coin.ToString()));
        }
        else if (this.Items != null)
        {
            var str = string.Join(", ", this.Items);

            sb.Append(string.Format(", Items: [{0}]", str));
        }
        else if ((this.Money == null) && (this.Items == null))
        {
            sb.Append(", Items: Nothing");
        }

        return sb.ToString();
    }

    #endregion
}
