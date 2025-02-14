namespace Zephyros1938.Marblerun;

public class MarbleRunContents
{
    public struct Trackdata
    {
        public Int32 length = 1;
        public Int32 duration = 10;
        public String username = "NO_USERNAME_SET";
        public String trackname = "NO_TRACKNAME_SET";
        public String imagedata = "data:image/png;base64,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";
        public String json = "{\"bricks\":{\"0\":{\"type\":\"Ball\",\"rotation\":0,\"row\":0,\"col\":0},\"37\":{\"type\":\"Brick\",\"rotation\":0,\"row\":3,\"col\":7},\"140\":{\"type\":\"Exit\",\"rotation\":0,\"row\":14,\"col\":0}},\"pairs\":[]}";
        public Trackdata(String? username = null, String? trackname = null, Int32? duration = null, Int32? length = null, String? imagedata = null, String? json = null)
        {
            if (length.HasValue) this.length = length.Value;
            if (duration.HasValue) this.duration = duration.Value;
            if (username != null) this.username = username;
            if (trackname != null) this.trackname = trackname;
            if (imagedata != null) this.imagedata = imagedata;
            if (json != null) this.json = json;
        }
    }
}