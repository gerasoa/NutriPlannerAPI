using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 
Ctrl + M, Ctrl + O: Colapsa todas as regiões de código.
Ctrl + M, Ctrl + P: Expande todas as regiões de código.
Ctrl + M, Ctrl + M: Colapsa ou expande a região de código onde o cursor está.
 */

namespace CCRS.Business.Models
{
    public class ConsultationInformation
    {
        /// <summary>
        /// The reason the patient sought medical guidance. (Portuguese: Motivação)
        /// </summary>
        public int Motivation { get; set; }

        /// <summary>
        /// The patient's goals and expectations regarding the treatment. (Portuguese: Expectativas)
        /// </summary>
        public int Expectations { get; set; }

        /// <summary>
        /// The clinical objectives from the doctor's or professional's perspective. (Portuguese: Objetivos clínicos)
        /// </summary>
        public int ClinicalObjectives { get; set; }

        /// <summary>
        /// Additional information the doctor may want to add about the consultation. (Portuguese: Outras informações da consulta)
        /// </summary>
        public int OtherConsultationInformation { get; set; }
    }

    public class PersonalSocialHistory
    {
        /// <summary>
        /// Bowel function frequency. (Portuguese: Função intestinal)
        /// </summary>
        public string BowelFunction { get; set; }

        /// <summary>
        /// Amount of sleep in hours per night. (Portuguese: Quantidade de sono)
        /// </summary>
        public string SleepDuration { get; set; }

        /// <summary>
        /// Indicates if the patient is a smoker. (Portuguese: Fumante)
        /// </summary>
        public bool Smoker { get; set; }

        /// <summary>
        /// Indicates if the patient consumes alcohol. (Portuguese: Bebe álcool)
        /// </summary>
        public bool AlcoholConsumption { get; set; }

        /// <summary>
        /// Marital status of the patient. (Portuguese: Estado civil)
        /// </summary>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// Frequency of physical activities. (Portuguese: Atividade física)
        /// </summary>
        public string PhysicalActivityFrequency { get; set; }

        /// <summary>
        /// The patient's ethnicity. (Portuguese: Raça)
        /// </summary>
        public string Ethnicity { get; set; }

        /// <summary>
        /// Additional personal/social information. (Portuguese: Outras informações pessoais/sociais)
        /// </summary>
        public string OtherPersonalSocialInformation { get; set; }
    }

    public class DietaryHistory
    {
        /// <summary>
        /// Usual wake-up time. (Portuguese: Hora habitual ao levantar)
        /// </summary>
        public string UsualWakeupTime { get; set; }

        /// <summary>
        /// Usual bedtime. (Portuguese: Hora habitual a deitar)
        /// </summary>
        public string UsualBedtime { get; set; }

        /// <summary>
        /// Type of diet followed by the patient. (Portuguese: Tipo de dieta)
        /// </summary>
        public string DietType { get; set; }

        /// <summary>
        /// The patient's preferred foods. (Portuguese: Alimentos preferidos)
        /// </summary>
        public string FavouriteFoods { get; set; }

        /// <summary>
        /// Foods poorly tolerated by the patient. (Portuguese: Alimentos mal aceitos)
        /// </summary>
        public string PoorlyToleratedFoods { get; set; }

        /// <summary>
        /// Allergies. (Portuguese: Alergias)
        /// </summary>
        public string Allergies { get; set; }

        /// <summary>
        /// Food intolerances. (Portuguese: Intolerâncias alimentares)
        /// </summary>
        public string FoodIntolerances { get; set; }

        /// <summary>
        /// Nutritional deficiencies. (Portuguese: Deficiências nutricionais)
        /// </summary>
        public string NutritionalDeficiencies { get; set; }

        /// <summary>
        /// Water intake in millilitres per day. (Portuguese: Ingestão de água)
        /// </summary>
        public string WaterIntake { get; set; }

        /// <summary>
        /// Additional dietary information. (Portuguese: Outras informações alimentares)
        /// </summary>
        public string OtherDietaryInformation { get; set; }
    }
}

