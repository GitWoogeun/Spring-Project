package spring.aop.entity;

public class NewlecExam implements Exam {
	
	private int kor;
	private int eng;
	private int math;
	private int com;
	
	public NewlecExam() {};


	public NewlecExam(int kor, int eng, int math, int com) {
		this.kor = kor;
		this.eng = eng;
		this.math = math;
		this.com = com;
	}

	@Override
	public int total() {
		
		// 곁다리 업무 호출
		//long start = System.currentTimeMillis();
		int result = kor+eng+math+com;
		
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

	@Override
	public float avg() {
		
		float result = total() / 4.0f;
		
		return result;	//소수점 넷째자리까지
	}

	public int getKor() {
		return kor;
	}

	public void setKor(int kor) {
		this.kor = kor;
	}

	public int getEng() {
		return eng;
	}

	public void setEng(int eng) {
		this.eng = eng;
	}

	public int getMath() {
		return math;
	}

	public void setMath(int math) {
		this.math = math;
	}

	public int getCom() {
		return com;
	}

	public void setCom(int com) {
		this.com = com;
	}

	@Override
	public String toString() {
		return "NewlecExam [kor=" + kor + ", eng=" + eng + ", math=" + math + ", com=" + com + "]";
	}
	
	
}