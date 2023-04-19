#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 AOP Before Advice 구현하기
#└─────────────────────────────────────────────────────────────────────

<bean id = "target" class = "spring.aop.entity.NewlecExam" p:kor="1" p:eng="1" p:math="1" p:com="1"/>
	<bean id="logAroundAdvice" class="spring.aop.advice.LogAroundAdvice"/>
	<bean id="logBeforeAdvice" class="spring.aop.advice.LogBeforeAdvice"/>
	<bean id="exam" class="org.springframework.aop.framework.ProxyFactoryBean">
    <!-- id가 target인 bean 태그를 참조 -->
    <property name="target" ref="target"/>
    <property name="interceptorNames">
        <list>
            <value>logAroundAdvice</value>
            <value>logBeforeAdvice</value>
        </list>
    </property>
</bean>

package spring.aop.advice;
import java.lang.reflect.Method;
import org.springframework.aop.MethodBeforeAdvice;
public class LogBeforeAdvice implements MethodBeforeAdvice{

	// 앞다리 AOP 
	@Override
	public void before(Method method, Object[] args, Object target) throws Throwable {
		System.out.println("앞에서 실행될 로직..");
	}
}
