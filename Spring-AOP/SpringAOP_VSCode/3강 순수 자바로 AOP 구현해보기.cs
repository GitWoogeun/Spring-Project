#┌─────────────────────────────────────────────────────────────────────
#│ AOP 코드 구현하기
#└─────────────────────────────────────────────────────────────────────
@Override
	public int total() {
		
		// 곁다리 업무 호출
		long start = System.currentTimeMillis();
		
		int result = kor+eng+math+com;
		
		try {
			Thread.sleep(200);
		} catch (InterruptedException e) {
			e.printStackTrace();
		}
		
		long end   = System.currentTimeMillis();
		System.out.println("총 걸린 시간 : " + (end - start) + "밀리초");
		return result;
	}


#┌─────────────────────────────────────────────────────────────────────
#│ Proxy를 이용한 AOP 곁다리 업무 구현
#└─────────────────────────────────────────────────────────────────────

package spring.aop;

import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Method;
import java.lang.reflect.Proxy;

import spring.aop.entity.Exam;
import spring.aop.entity.NewlecExam;

public class Program {

	public static void main(String[] args) {
		
		Exam exam = new NewlecExam(1,1,1,1);
		
		// 프락시 생성 ( Exam 기능을 사용하면서 곁다리 업무 가능 )
        // proxy는 Exam의 기능을 구현할 수 있다.
		Exam proxy = (Exam) Proxy.newProxyInstance(NewlecExam.class.getClassLoader(),
											new Class[] {Exam.class},
											new InvocationHandler() {
												
												@Override
												public Object invoke(Object proxy, Method method, Object[] args) throws Throwable {
													
													long start = System.currentTimeMillis();
													
													Object result = method.invoke(exam, args);
													
													long end   = System.currentTimeMillis();
													
													String message = ( end - start ) + "ms 시간이 소요";
													
													System.out.println(message);
													
													return result;
												}
											}
										);

//		System.out.printf("total is : %d", exam.total());
		System.out.printf("total is : %d", proxy.total());
		System.out.printf("avg   is : %f", proxy.avg());
		
	}

}
