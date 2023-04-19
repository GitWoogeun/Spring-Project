#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 AOP Point Cut ( Weaving, Join Point )
#└─────────────────────────────────────────────────────────────────────

AOP 구현 방식
포인트 컷 (Pointcuts)과 조인 포인트(JoinPoint) 그리고 위빙(weaving);

#┌─────────────────────────────────────────────────────────────────────
#│ 스프링 AOP setting.xml
#└─────────────────────────────────────────────────────────────────────
<bean id = "target" class = "spring.aop.entity.NewlecExam" p:kor="1" p:eng="1" p:math="1" p:com="1"/>
	<bean id="logAroundAdvice" class="spring.aop.advice.LogAroundAdvice"/>
	<bean id="logBeforeAdvice" class="spring.aop.advice.LogBeforeAdvice"/>
	<bean id="logAfterReturningAdvice" class="spring.aop.advice.LogAfterReturningAdvice"/>
	<bean id="logAfterThrowingAdvice" class="spring.aop.advice.LogAfterThrowingAdvice"/>
	
	<bean id="classicBeforeAdvisor" class="org.springframework.aop.support.DefaultPointcutAdvisor">
		<property name="advice" ref="logBeforeAdvice"/>
		<property name="pointcut" ref="classicPointCut"/>
	</bean>
	
	<!-- Point Cut 생성 -->
	<bean id="classicPointCut" class="org.springframework.aop.support.NameMatchMethodPointcut">
		<property name="mappedName" value="total"></property>
	</bean>
	
	
	
	<bean id="exam" class="org.springframework.aop.framework.ProxyFactoryBean">
    <!-- id가 target인 bean 태그를 참조 -->
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