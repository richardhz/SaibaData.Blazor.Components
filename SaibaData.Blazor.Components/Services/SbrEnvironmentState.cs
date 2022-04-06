using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaibaData.Blazor.Components.Services;
public class SbrEnvironmentState
{
    public event EventHandler<EventArgs>? LanguageSwitched;

    public virtual void OnLanguageSwitched()
    {
        LanguageSwitched?.Invoke(this, EventArgs.Empty);
    }
}
