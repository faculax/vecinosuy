package logic;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by Facu on 5/9/17.
 */

public class Repository {
    public Map<Integer,String> announcementBody;
    public Map<Integer,String> announcementImage;
    public int minute = -1;
    public int hour = -1;
    public int month = -1;
    public int year = -1;
    public int day = -1;
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

    public void reset(){
         minute = -1;
         hour = -1;
         month = -1;
         year = -1;
         day = -1;
    }

    public boolean validTimeAndDate(){
        return minute != -1 &&
                hour != -1 &&
                month != -1 &&
                year != -1 &&
                day != -1;
    }
}
