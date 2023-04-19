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
