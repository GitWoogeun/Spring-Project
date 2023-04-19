#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 AOP 간소화된 Advisor
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
	<bean id = "logAroundAdvice" class="spring.aop.advice.LogAroundAdvice"/>
	<bean id = "logBeforeAdvice" class="spring.aop.advice.LogBeforeAdvice"/>
	<bean id = "logAfterReturningAdvice" class="spring.aop.advice.LogAfterReturningAdvice"/>
	<bean id = "logAfterThrowingAdvice" class="spring.aop.advice.LogAfterThrowingAdvice"/>
	

<!-- Point Cut과 Advisor -->
/* <bean id="classicPointCut" class="org.springframework.aop.support.NameMatchMethodPointcut">
		<property name="mappedName" value="total"></property>
	</bean>
	
	
	<bean id="classicBeforeAdvisor" class="org.springframework.aop.support.DefaultPointcutAdvisor">
		<property name="advice" ref="logBeforeAdvice"/>
		<property name="pointcut" ref="classicPointCut"/>
	</bean>
	
	<bean id="classicAroundAdvisor" class="org.springframework.aop.support.DefaultPointcutAdvisor">
		<property name="advice" ref="logBeforeAdvice"/>
		<property name="pointcut" ref="classicPointCut"/>
	</bean>
		<bean id="classicBeforeAdvisor" class="org.springframework.aop.support.NameMatchMethodPointcutAdvisor">
		<property name="advice" ref="logBeforeAdvice"/>
		<property name="mappedNames">
			<list>
				<value>total</value>
				<value>avg</value>
			</list>
		</property>
	</bean>
*/	
	
    // RegexpMethodPointcutAdvisor : 정규식
	<bean id="classicBeforeAdvisor" class="org.springframework.aop.support.RegexpMethodPointcutAdvisor">
		<property name="advice" ref="logBeforeAdvice"/>
		<property name="patterns">
			<list>
                // 메소드명 to를 포함한 모든 메소드 BeforeAdvice 적용
				<value>.*to.*</value>
			</list>
		</property>
	</bean>
	
    // NameMatchMethodPointcutAdvisor는 Spring 프레임워크에서 AOP(Aspect-Oriented Programming)를 구현하는 데 사용되는 클래스 중 하나
    // 이 클래스는 메서드 이름을 기반으로 메서드를 대상으로 하는 어드바이스를 적용하기 위해 사용
	<bean id="classicAroundAdvisor" class="org.springframework.aop.support.NameMatchMethodPointcutAdvisor">
		<property name="advice" ref="logAroundAdvice"/>
		<property name="mappedName" value="total"></property>
	</bean>

	<bean id="exam" class="org.springframework.aop.framework.ProxyFactoryBean">
		// id가 target인 bean 태그를 참조
		// <bean id = "target" class = "spring.aop.entity.NewlecExam" p:kor="1" p:eng="1" p:math="1" p:com="1"/>
        <property name="target" ref="target"/>
		<property name="interceptorNames">
			<list>
            // NewlecExam Class에서 아래 advice 항목을 적용
				<value>logAroundAdvice</value>
				<value>classicBeforeAdvisor</value>
				<value>logBeforeAdvice</value>
				<value>logAfterReturningAdvice</value>
				<value>logAfterThrowingAdvice</value>
			</list>
		</property>
	</bean>
</beans>