package logic;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by Facu on 5/9/17.
 */

public class Repository {
    public Map<Integer,String> announcementBody;
    public Map<Integer,String> announcementImage;
    private static Repository instance = null;
    protected Repository() {
        announcementBody = new HashMap<Integer, String>();
        announcementImage = new HashMap<Integer, String>();
    }
    public static Repository getInstance() {
        if(instance == null) {
            instance = new Repository();
        }
        return instance;
    }
}
