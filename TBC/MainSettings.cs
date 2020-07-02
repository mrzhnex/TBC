using Smod2;
using Smod2.Attributes;
namespace TBC
{
    [PluginDetails(
        author = "Innocence",
        description = "description",
        id = "team.broad.cast",
        name = "TeamBroadCast",
        configPrefix = "tbc",
        SmodMajor = 3,
        SmodMinor = 0,
        SmodRevision = 0,
        version = "4.1.B.1"
    )]

    public class MainSettings : Plugin
    {
        public override void Register()
        {
            AddEventHandlers(new SetEvents(this));
            AddCommand("tbc", new TBCCommand());
            AddCommand("bspawn", new BSpawnCommand());
            AddCommand("rbc", new RBCCommand());
            AddCommand("sbc", new SBCCommand());
            AddCommand("dbc", new DBCCommand());
            AddCommand("lbc", new LBCCommand());
        }

        public override void OnEnable()
        {
            Info(Details.name + " on");
        }

        public override void OnDisable()
        {
            Info(Details.name + " off");
        }
    }
}
