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
