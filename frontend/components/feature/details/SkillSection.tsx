import { skill } from "@/types/list/Offer/skill";

type skillSection = {
    offerSkills: skill[]
}

export function SkillsSection({offerSkills}: skillSection) {
    return (
        <div className="required-skills-section">
            <div className="frame-168">
                <div className="frame-166">
                    <div className="required-skills">Required skills</div>
                    <div className="line-10" />
                </div>
                <div className="skill-section-content">
                    <div className="skill-section-row">
                        <div className="skill-section-item">
                            <img className="pie-chart2" src="/icons/light/clockLight.svg" />
                            <div className="city-name">SkillMonths</div>
                        </div>
                        <div className="skill-section-item2">
                            <img className="award" src="/icons/light/awardLight.svg" />
                            <div className="city-name">SkillLevel</div>
                        </div>
                        <div className="skill-section-item3">
                            <img className="check-circle" src="/icons/light/check-circleLight.svg" />
                            <div className="city-name">Skillname</div>
                        </div>
                    </div>
                    <div className="skill-section-row">
                        <div className="skill-section-item">
                            <img className="pie-chart3" src="/icons/light/clockLight.svg" />
                            <div className="city-name">SkillMonths</div>
                        </div>
                        <div className="skill-section-item2">
                            <img className="award2" src="/icons/light/awardLight.svg" />
                            <div className="city-name">SkillLevel</div>
                        </div>
                        <div className="skill-section-item3">
                            <img className="check-circle2" src="/icons/light/check-circleLight.svg" />
                            <div className="city-name">Skillname</div>
                        </div>
                    </div>
                </div>
            </div>
            <div className="frame-172">
                <div className="main-button">
                    <div className="find-mathing-job">Show more (20)</div>
                </div>
            </div>
        </div>
    );
}