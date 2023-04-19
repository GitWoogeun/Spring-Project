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
