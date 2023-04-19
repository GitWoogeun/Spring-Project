package spring.aop;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import spring.aop.entity.Exam;

public class Program {

	public static void main(String[] args) {
		
		// 스프링 setting
		ApplicationContext context 
					= new ClassPathXmlApplicationContext("spring/aop/setting.xml"); 
					// new AnnotationConfigApplicationContext(NewlecDIConfig.class);
		
		
		
		Exam exam = (Exam)context.getBean("exam");
		
		System.out.printf("total is : %d", exam.total());
		System.out.println("\n");
		System.out.printf("avg   is : %f", exam.avg());
		
		
//		Exam exam = new NewlecExam(1,1,1,1);		
//		// 프락시 생성 ( Exam 기능을 사용하면서 곁다리 업무 가능 )
//		Exam proxy = (Exam) Proxy.newProxyInstance(NewlecExam.class.getClassLoader(),
//											new Class[] {Exam.class},
//											new InvocationHandler() {
//												
//												@Override
//												public Object invoke(Object proxy, Method method, Object[] args) throws Throwable {
//													
//													long start = System.currentTimeMillis();
//													
//													Object result = method.invoke(exam, args);
//													
//													long end   = System.currentTimeMillis();
//													
//													String message = ( end - start ) + "ms 시간이 소요";
//													
//													System.out.println(message);
//													
//													return result;
//												}
//											}
//										);
//
//		System.out.printf("total is : %d", exam.total());
		
	}

}
