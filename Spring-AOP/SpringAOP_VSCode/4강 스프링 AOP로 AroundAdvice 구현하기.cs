#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 AOP로 AroundAdvice 구현하기
#└─────────────────────────────────────────────────────────────────────

Spring에서 Advice 구현

Before           : 앞에만 필요한 곁다리 업무

After returnning : 뒤에만 필요한 곁다리 업무

After throwing   : 예외를 처리하는 곁다리 업무

Around           : 앞 뒤로 다 필요한 곁다리 업무

#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 AOP로 AroundAdvice 구현하기 ( main Program.java)
#└─────────────────────────────────────────────────────────────────────
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
		System.out.printf("avg   is : %f", exam.avg());
		
//		Exam exam = new NewlecExam(1,1,1,1);		
//		// 프락시 생성 ( Exam 기능을 사용하면서 곁다리 업무 가능 )
//		Exam proxy = (Exam) Proxy.newProxyInstance(NewlecExam.class.getClassLoader(),
//											new Class[] {Exam.class},
//											new InvocationHandler() {
//												
//												@Override
//												public Object invoke(Object proxy, Method method, Object[] args) throws Throwable {
//													long start = System.currentTimeMillis();
//													Object result = method.invoke(exam, args);
//													long end   = System.currentTimeMillis();
//													String message = ( end - start ) + "ms 시간이 소요";
//													System.out.println(message);			
//													return result;
//												}
//											}
//										);
//		System.out.printf("total is : %d", exam.total());	
	}
}

#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 AOP로 AroundAdvice 구현하기 ( LogAroundAdvice (Around AOP) )
#└─────────────────────────────────────────────────────────────────────
package spring.aop.advice;
import org.aopalliance.intercept.MethodInterceptor;
import org.aopalliance.intercept.MethodInvocation;
public class LogAroundAdvice implements MethodInterceptor{
	@Override
	public Object invoke(MethodInvocation invocation) throws Throwable {
		
        long start = System.currentTimeMillis();   // 프로그램 시작
		Object result = invocation.proceed();      // 주 업무 
		long end   = System.currentTimeMillis();   // 프로그램 종료
		
        String message = ( end - start ) + "ms초";
		
        System.out.println(message);		
		
        return result;
	}
}

#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 AOP로 AroundAdvice 구현하기 ( setting.xml (Proxy 설정) )
#└─────────────────────────────────────────────────────────────────────
<?xml version="1.0" encoding="UTF-8"?>
<beans xmlns="http://www.springframework.org/schema/beans"
    xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
    xmlns:p="http://www.springframework.org/schema/p"
    xmlns:util="http://www.springframework.org/schema/util"
    xmlns:context="http://www.springframework.org/schema/context"
    xsi:schemaLocation="http://www.springframework.org/schema/beans http://www.springframework.org/schema/beans/spring-beans.xsd
        http://www.springframework.org/schema/util http://www.springframework.org/schema/util/spring-util.xsd
        http://www.springframework.org/schema/context http://www.springframework.org/schema/context/spring-context.xsd">
        
	
	<bean id = "target" class = "spring.aop.entity.NewlecExam" p:kor="1" p:eng="1" p:math="1" p:com="1"/>
	<bean id="logAroundAdvice" class="spring.aop.advice.LogAroundAdvice"/>
	<bean id="exam" class="org.springframework.aop.framework.ProxyFactoryBean">
		
        // id가 target인 bean 태그를 참조
		<property name="target" ref="target"/>
		<property name="interceptorNames">
			<list>
				<value>logAroundAdvice</value>
			</list>
		</property>
	
    </bean>
</beans>