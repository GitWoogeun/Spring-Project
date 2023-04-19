#┌─────────────────────────────────────────────────────────────────────
#│ Spring을 사용하지 않고 AOP 구현하기
#└─────────────────────────────────────────────────────────────────────

// Cross-cutting Concern ( 곁다리 업무 )
public class NewlecExam implements Exam {

    public int total()
    {
        // 시간을 출력하는 로그
        long start = System.currentTimeMills();

        SimpleDateFormat dayTime = new SimpleDateFormat("yyyy-mm-dd hh:mm:ss");
        String str = dayTime.format(start);
        System.out.println(str);
        /************************************/

        int result = kor + eng + math + com;
        
        /************************************/
        long end = System.currentTimeMills();

        String message = (end - start) + "ms가 걸림";
        System.out.println("걸린 시간 : " message);
        return result;
    }
}


#┌─────────────────────────────────────────────────────────────────────
#│ 프락시 클래스에 구현되는 Cross-cutting Concern
#└─────────────────────────────────────────────────────────────────────
long start = System.currentTimeMills();

SimpleDateFormat dayTime = new SimpleDateFormat("yyyy-mm-dd hh:mm:ss");
String str = dayTime.format(start);
System.out.println(str);

long end = System.currentTimeMills();

String message = (end - start) + "ms가 걸림";
System.out.println("걸린 시간 : " message);
return result;