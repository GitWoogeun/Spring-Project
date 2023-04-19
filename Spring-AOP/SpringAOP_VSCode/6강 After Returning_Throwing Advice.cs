#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 AOP After Returning  / Throwing Advice AOP
#└─────────────────────────────────────────────────────────────────────

#┌─────────────────────────────────────────────────────────────────────
#│ setting.xml ( afterThrowing 예외 클래스 생성 )
#└─────────────────────────────────────────────────────────────────────
<bean id = "target" class = "spring.aop.entity.NewlecExam" p:kor="1" p:eng="1" p:math="1" p:com="1"/>
	<bean id="logAroundAdvice" class="spring.aop.advice.LogAroundAdvice"/>
	<bean id="logBeforeAdvice" class="spring.aop.advice.LogBeforeAdvice"/>
	<bean id="logAfterReturningAdvice" class="spring.aop.advice.LogAfterReturningAdvice"/>
	<bean id="logAfterThrowingAdvice" class="spring.aop.advice.LogAfterThrowingAdvice"/>
	
	<bean id="exam" class="org.springframework.aop.framework.ProxyFactoryBean">
    <!-- id가 target인 bean 태그를 참조 -->
    <property name="target" ref="target"/>
    <property name="interceptorNames">
        <list>
            <value>logAroundAdvice</value>
            <value>logBeforeAdvice</value>
            <value>logAfterReturningAdvice</value>
            <value>logAfterThrowingAdvice</value>
        </list>
    </property>
</bean>

#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 LogAfterThrowingAdvice 예외 클래스 생성
#└─────────────────────────────────────────────────────────────────────
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

#┌─────────────────────────────────────────────────────────────────────
#│ Target 클래스 ( NewlecExcam )
#└─────────────────────────────────────────────────────────────────────
@Override
public int total() {
    
    // 곁다리 업무 호출
    //long start = System.currentTimeMillis();
    int result = kor+eng+math+com;
    
    // 국어점수가 100점이 넘을 시 IllegarArgumentException 발생
    if(kor > 100){
        throw new IllegalArgumentException("유효하지 않은 국어점수");
    }
    
    try {
        Thread.sleep(200);
    } catch (InterruptedException e) {
        e.printStackTrace();
    }
    
    //long end   = System.currentTimeMillis();
    
    //System.out.println("총 걸린 시간 : " + (end - start) + "밀리초");
    return result;
}
