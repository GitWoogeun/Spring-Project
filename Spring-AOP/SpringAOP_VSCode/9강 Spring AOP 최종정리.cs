#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 AOP 최종 정리
#└─────────────────────────────────────────────────────────────────────

"AOP란?"
Spring AOP(Aspect-Oriented Programming)는 객체 지향 프로그래밍에서 
측면(Aspect)을 분리하여 애플리케이션의 핵심 로직과 분리된 부가 기능을 제공하는 
프로그래밍 패러다임입니다;

"AOP 사용 이유 :"
Spring AOP는 런타임 시점에 프록시를 생성하여 핵심 로직 실행 전/후에 
부가 기능(로깅, 보안, 트랜잭션 처리 등)을 적용;

"AOP 설정 :"
Spring AOP는 AspectJ와 같은 AOP 프레임워크를 지원하며, 주로 XML 또는 
어노테이션을 사용하여 설정;


#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 AOP XML 설정
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
        
	
"logAroundAdvice"
=> AroundAdvice는 대상 객체의 메서드 호출을 제어하고, 메서드 호출 전/후에 추가적인 동작을 수행;

"logBeforeAdivce"          
=> BeforeAdvice를 사용하면 대상 메서드 실행 전에 수행;
    
"logAfterReturningAdvice"
=> 메서드 실행 결과를 로그로 기록하는 기능을 제공합니다. 대상 메서드가 정상적으로 실행되고 
   반환 값을 갖는 경우에만 로그를 남기기 때문에, 예외 발생 시에는 로그가 남지 않습니다;

"logAfterThrowingAdvice"
=> 예외 발생 시에만 로그를 남기기 때문에, 메서드가 정상적으로 실행되고 반환 값을 갖는 경우에는 
   로그가 남지 않습니다.;
	
    # 각 Advice 빈 등록
    <bean id = "target" class = "spring.aop.entity.NewlecExam" p:kor="1" p:eng="1" p:math="1" p:com="1"/>
	<bean id="logAroundAdvice" class="spring.aop.advice.LogAroundAdvice"/>
	<bean id="logBeforeAdvice" class="spring.aop.advice.LogBeforeAdvice"/>
	<bean id="logAfterReturningAdvice" class="spring.aop.advice.LogAfterReturningAdvice"/>
	<bean id="logAfterThrowingAdvice" class="spring.aop.advice.LogAfterThrowingAdvice"/>


    // 메소드가 실행 전 logBeforeAdvice 적용
    // patterns로 메소드 명에 to 또는 av를 포함한다면 BeforeAdvice 적용
    <bean id="classicBeforeAdvisor" class="org.springframework.aop.support.RegexpMethodPointcutAdvisor">
		<property name="advice" ref="logBeforeAdvice"/>
		<property name="patterns">
			<list>
				<value>.*to.*</value>
				<value>.*av.*</value>
			</list>
		</property>
	</bean>


    <bean id="exam" class="org.springframework.aop.framework.ProxyFactoryBean">
		// id가 target인 bean 태그를 참조하고 아래 advice들을 적용
        // id가 target인 bean 태그의 참조 클래스 : NewlecExam 클래스
		<property name="target" ref="target"/>
		<property name="interceptorNames">
			<list>
				<value>logAroundAdvice</value>
				<value>classicBeforeAdvisor</value>
				<value>logBeforeAdvice</value>
				<value>logAfterReturningAdvice</value>
				<value>logAfterThrowingAdvice</value>
			</list>
		</property>
	</bean>
</beans>


#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 AOP 각 Advice 클래스
#└─────────────────────────────────────────────────────────────────────
package spring.aop.advice;
import java.lang.reflect.Method;
import org.springframework.aop.MethodBeforeAdvice;

public class LogBeforeAdvice implements MethodBeforeAdvice{
	// 메소드가 실행 전 AOP 적용
	@Override
	public void before(Method method, Object[] args, Object target) throws Throwable {
		System.out.println("앞에서 실행될 로직..");
	}
}

package spring.aop.advice;
import org.aopalliance.intercept.MethodInterceptor;
import org.aopalliance.intercept.MethodInvocation;

public class LogAroundAdvice implements MethodInterceptor{

	// Around AOP
	@Override
	public Object invoke(MethodInvocation invocation) throws Throwable {
		long start = System.currentTimeMillis();
		Object result = invocation.proceed();
		long end   = System.currentTimeMillis();
		String message = ( end - start ) + "ms초";
		System.out.println(message);
		return result;
	}
}

package spring.aop.advice;
import java.lang.reflect.Method;
import org.springframework.aop.AfterReturningAdvice;
public class LogAfterReturningAdvice implements AfterReturningAdvice{
	@Override
	public void afterReturning(Object returnValue, Method method, Object[] args, Object target) throws Throwable {	
		System.out.println("returnValue : " + returnValue + "\n, method : " + method);
	}
}

package spring.aop.advice;
import org.springframework.aop.ThrowsAdvice;
// ThrowAdvice는 구현해야될 함수가 정해져있지 않아서
// ThrowsAdvice는 default 메소드가 구현이 되어있음
public class LogAfterThrowingAdvice implements ThrowsAdvice{

	// IllegalArgumentException은 메서드에 전달된 인수가 메서드가 예상하는 것과 일치하지 않을 때 발생하는 예외
	// 예를 들어, 메서드가 양의 정수를 받아들이는 경우 음의 정수가 전달되면 IllegalArgumentException이 발생
	public void afterThrowing(IllegalArgumentException e) throws Throwable{
		
		// BeforeAdvice가 먼저 홏출이 되고
		// Target의 메소드가 실행 됩니다.
		// Target의 메소드가 실행 중 예외가 발생하면 => afterThrowing이 실행된다.		
		System.out.println("IllegarArgumentException 예외가 발생하였습니다.\n" + e.getMessage());
	}
}

package spring.aop.advice;
import java.lang.reflect.Method;
import org.springframework.aop.AfterReturningAdvice;
public class LogAfterReturningAdvice implements AfterReturningAdvice{

	@Override
	public void afterReturning(Object returnValue, Method method, Object[] args, Object target) throws Throwable {	
		System.out.println("returnValue : " + returnValue + "\n, method : " + method);
	}	
}



