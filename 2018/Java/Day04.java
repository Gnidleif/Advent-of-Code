import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Date;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class main {
	public static void main(String[] args) {
		String data = "";
		try {
			data = readFileAsString("Day04.txt");
		} catch (Exception e) {
			System.out.println(e);
		}
		
		//String[] lines = data.split("\\r?\\n");
		try {
			Part1(data);
		} catch(Exception e) {
			System.out.println(e);
		}
	}
	
	public static String readFileAsString(String filename) throws IOException {
		return new String(Files.readAllBytes(Paths.get(filename)));
	}
	
	public static int Part1(String input) throws Exception {
		Pattern rgx = Pattern.compile("^\\[((\\d+).*)\\]\\s(.*)$", Pattern.MULTILINE);
		Matcher m = rgx.matcher(input);
		SimpleDateFormat fmt = new SimpleDateFormat("yyyy-MM-dd HH:mm");
		ArrayList<DateLine> things = new ArrayList<DateLine>();
		while(m.find()) {
			things.add(new DateLine(fmt.parse(m.group(1)), m.group(3)));
		}
		Collections.sort(things);
		
		for(int i = 0; i < things.size(); i++) {
			System.out.println(things.get(i).command);
		}
		return 0;
	}
	
	public static class DateLine implements Comparable<DateLine> {
		public Date dateTime;
		public String command;
		
		public DateLine(Date dt, String cmd) {
			this.dateTime = dt;
			this.command = cmd;
		}

		@Override
		public int compareTo(DateLine o) {
			return this.dateTime.compareTo(o.dateTime);
		}
		
		@Override
		public String toString() {
			return this.dateTime.toString() + ": " + this.command;
		}
	}
	
	public static class Guard {
		public int id;
		public int count = 0;
		public int[] minutes = new int[60];
		
		public Guard(int id) {
			this.id = id;
		}
		
		public void ToggleSleep(int start, int end, boolean awake) {
			this.count++;
			if(!awake) {
				for(int i = start; i <= end; i++) {
					this.minutes[i]++;
				}
			}
		}
	}
}
