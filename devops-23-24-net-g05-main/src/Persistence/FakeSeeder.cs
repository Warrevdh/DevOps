using Domain.Users.Employees.Availabilities;
using Domain.Appointments;
using Domain.Articles.Fragments;
using Domain.EyeConditions;
using Domain.Users.Doctors;
using Domain.Users.Employees;
using Domain.Users.Patients;
using Persistence;
using Domain.Articles;

public class FakeSeeder
{
	private readonly ApplicationDbContext dbContext;

	public FakeSeeder(ApplicationDbContext dbContext)
	{
		this.dbContext = dbContext;
	}

	public void Seed()
	{
		// Not a good idea in production.
		dbContext.Database.EnsureDeleted();
		dbContext.Database.EnsureCreated();

		SeedEyeConditions();
		SeedTeamMembers();
		SeedSpecializations();
		SeedAppointments();
		SeedPatients();
		SeedSymptoms();
		SeedArticles();
		
	}

	private void SeedTeamMembers()
	{
		List<Group> groups = new()
		{
			new Group("Oogartsen", 1),
			new Group("Medewerkers", 2),
		};

		List<Employee> members = new()
		{
			new Doctor("Ozlem", "Kose", new DateTime(1985, 7, 15), "0495741468", "ozlemkose@gmail.com", groups[0], "https://g05devops.blob.core.windows.net/g05devopsblob/b5ee6440-6dca-45fb-9898-2d741ac128b2.jpeg"),
			new Doctor("Eline", "De Pauw",new DateTime(1995, 2, 10), "0823456789", "elinedepauw@gmail.com", groups[0], "https://g05devops.blob.core.windows.net/g05devopsblob/4e677571-7bb3-4958-883f-d6b2d55c719c.jpeg"),
			new Doctor("Diete", "Paternoster",new DateTime(1980, 6, 3), "0934567890", "davidjones@gmail.com", groups[0], "https://g05devops.blob.core.windows.net/g05devopsblob/95d556c3-ab69-4860-9a29-f109e4c1336e.png "),
		};

		dbContext.Groups.AddRange(groups);

		members[0].Bio = new Bio("Dokter Ozlem Kose behaalde haar diploma in de geneeskunde in 2013 aan de Vrije Universiteit Brussel. Na haar opleiding geneeskunde specialiseerde ze zich gedurende vier jaar in de oftalmologie aan de Université Libre de Bruxelles. Dr. Kose behaalde een bijkomende interuniversitaire diploma \"Inflammations et Infections oculaires\" aan de Universiteit Paris Diderot in Frankrijk. Verder is ze Fellow van de European Board of Ophthalmology. Na haar specialisatie trok ze in 2017 een maand naar India voor een fellowship in de cataract en refractieve chirurgie onder begeleiding van Prof. Dr. Agarwal. Vervolgens genoot ze gedurende 6 maanden van een bijkomende chirurgische vorming onder begeleiding van Dr. Gatinel in het Fondation Rothschild te Parijs. Verder sub specialiseerde ze zich in de orbito palpebrale chirurgie (oogleden, traanwegen en orbita) op de afdelingen oftalmologie van het CHU Brugmann ziekenhuis en het Sint-Pietersziekenhuis te Brussel. Ten slotte volgde ze gedurende één jaar een fellowship in het Oogziekenhuis van Rotterdam om zich verder te bekwamen in de oculoplastische chirurgie. Actueel is ze sinds 2019 verbonden aan het AZ Sint Lucas Ziekenhuis in Gent waar ze haar vooral toespitst op chirurgie van de oogleden, traanwegen, cataract, maar ook op algemene oogheelkundige aandoeningen.");
        // Save the employees to the database
        dbContext.Employees.AddRange(members);
        dbContext.SaveChanges();

        // Retrieve the saved employees to get their assigned IDs
        var savedMembers = dbContext.Employees.ToList();

        for (int i = 0; i < savedMembers.Count; i++)
        {
            var member = savedMembers[i];

            // Skip adding availabilities for the last member
            if (i == savedMembers.Count - 1)
                continue;

            var availabilities = new List<Availability>
			{
				new Availability(new DateTime(2023, 12, 13, 7, 0, 0), new DateTime(2023, 12, 13, 13, 0, 0)),
				new Availability(new DateTime(2023, 12, 14, 7, 0, 0), new DateTime(2023, 12, 14, 13, 0, 0)),
				new Availability(new DateTime(2023, 12, 15, 8, 0, 0), new DateTime(2023, 12, 15, 13, 0, 0)),
				new Availability(new DateTime(2023, 12, 16, 9, 0, 0), new DateTime(2023, 12, 16, 14, 0, 0)),
				new Availability(new DateTime(2023, 12, 21, 10, 0, 0), new DateTime(2023, 12, 21, 15, 0, 0)),
				// Add more availabilities as needed
			};

            // Assuming Employee has a method to add availabilities
            foreach (var availability in availabilities)
            {
                member.Availability(availability); // Replace with the correct method to add availability to an employee
            }

            dbContext.Employees.Update(member);
        }

        dbContext.SaveChanges();
    }

	private void SeedSpecializations()
	{
		List<Specialization> specializations = new()
		{
			new Specialization("Oogchirurg"),
			new Specialization("Optometrist"),
			new Specialization("Orthoptist"),
		};

		Doctor d = (Doctor)dbContext.Employees.ToList()[0];
		d.Specialization(specializations[0]);
		Doctor d2 = (Doctor)dbContext.Employees.ToList()[1];
		d2.Specialization(specializations[1]);
		Doctor d3 = (Doctor)dbContext.Employees.ToList()[2];
		d2.Specialization(specializations[2]);

		dbContext.Specializations.AddRange(specializations);
		dbContext.Employees.Update(d);
		dbContext.Employees.Update(d2);
		dbContext.Employees.Update(d3);
		dbContext.SaveChanges();
	}

	private void SeedEyeConditions()
	{
		//var conditions = new EyeConditionFaker().AsTransient().UseSeed(1337).Generate(100);
		List<EyeCondition> conditions = new()
		{
			new EyeCondition("Glaucoom", "Glaucoom is een oogaandoening met verhoogde oogdruk die het gezichtsvermogen kan beschadigen en leiden tot blindheid zonder behandeling.", "<div>Glaucoom is een veelvoorkomende oogaandoening die vaak onopgemerkt blijft tot het vergevorderde stadia bereikt. Het is een sluipende bedreiging voor uw gezichtsvermogen omdat het meestal geen symptomen veroorzaakt in de vroege fase. Het is daarom van vitaal belang om te begrijpen wat glaucoom is, hoe het wordt gediagnosticeerd en behandeld, en wat u kunt doen om uw ooggezondheid te beschermen.</div><div><br></div><div><span style=\"font-weight: bold; font-size: large;\">Wat is Glaucoom?</span></div><div>Glaucoom is een groep oogaandoeningen die schade aan de oogzenuw veroorzaken, wat leidt tot verlies van gezichtsvermogen. Meestal is deze schade gerelateerd aan verhoogde druk in het oog, maar niet altijd. Het kan in beide ogen voorkomen, en als het onbehandeld blijft, kan het leiden tot permanente blindheid.</div><div><br></div><div><span style=\"font-weight: bold; font-size: large;\">Soorten Glaucoom</span></div><div>Er zijn verschillende vormen van glaucoom, waarvan de twee meest voorkomende zijn:</div><div><br></div><div><ul><li>Primair Openhoekglaucoom: Dit is de meest voorkomende vorm en ontwikkelt zich langzaam zonder symptomen. Het gezichtsvermogen wordt geleidelijk aangetast.</li><li>Acuut Geslotenhoekglaucoom: Dit is zeldzamer maar ernstiger. Het kan plotseling optreden en symptomen veroorzaken zoals oogpijn, wazig zicht en misselijkheid. Het vereist onmiddellijke medische aandacht.</li></ul></div>","https://oogziekenhuis.me/omc-amsterdam.nl/Glaucoom_files/shapeimage_10.png", ""),
			new EyeCondition("Cataract", "Cataract is troebelheid van de ooglens, wat wazig zicht veroorzaakt, maar het kan met succes worden behandeld met cataractchirurgie.", "Voeg hier tekst toe","https://ogen-blik.expert/wp-content/uploads/2018/12/banner_ogen_blik_cataract_symptomen.jpg", "https://drive.google.com/uc?export=download&id=1MvoydmefB_ppopx5lfO0OODS650sw_P8"),
			new EyeCondition("Maculadegeneratie", "Maculadegeneratie is schade aan het centrale netvliesgebied (macula) en kan leiden tot verlies van centraal zicht, vooral bij natte maculadegeneratie. Vroege behandeling is belangrijk.", "Voeg hier tekst toe","https://www.oogartsliesenborghs.be/upload/20160621_184183394257694cb4a619d.png", ""),
			new EyeCondition("Diabetische Retinopathie", "Diabetische retinopathie is een oogziekte die optreedt bij mensen met diabetes en schade aan de bloedvaten van het netvlies veroorzaakt, wat kan leiden tot gezichtsproblemen en zelfs blindheid als het niet wordt behandeld.", "Voeg hier tekst toe","https://www.braille.be/uploads/assets/693/1366896198-diabetische-retinopathie-nl-print-02-500x400.jpg", ""),
			new EyeCondition("Bijziendheid", "Bijziendheid, ook wel bekend als myopie, is een oogafwijking waarbij je dichtbij goed kunt zien, maar objecten in de verte vaag lijken.", "Voeg hier tekst toe","https://www.optometrie.nl/serverspecific/default/images/Image/fotosheaders/myopie-bewerkt-.jpg", ""),
			new EyeCondition("Verziendheid", "Verziendheid, ook wel hypermetropie genoemd, is een oogafwijking waarbij objecten dichtbij wazig lijken, terwijl je objecten in de verte beter kunt zien.", "Voeg hier tekst toe","https://www.optometrie.nl/serverspecific/default/images/Image/fotosheaders/headerverziend-bewerkt-.png", ""),
			new EyeCondition("Astigmatisme", "Astigmatisme is een veelvoorkomende brekingsfout van het oog, die de manier waarop licht het oog binnenkomt beïnvloedt. Anders dan bij een normaal oog met een perfect bolvormig hoornvlies, vertoont het hoornvlies van iemand met astigmatisme onregelmatigheden in vorm. Dit resulteert in onscherpe en vervormde beelden, waardoor een specifieke correctie nodig is voor helder zicht.", "<div><span style=\"font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);\">Astigmatisme is een veelvoorkomende brekingsfout van het oog, waarbij het hoornvlies onregelmatigheden vertoont in plaats van de normale bolvorm. Deze onregelmatigheden, of het nu een ovaalvormig hoornvlies is of vervormingen in de ooglens, resulteren in onscherpe en vervormde beelden.</span><br></div><div><br></div><div>De oorzaken van astigmatisme zijn divers. Erfelijkheid speelt een belangrijke rol, en het hoornvlies kan zich anders vormen dan normaal. Veranderingen in de vorm van de ooglens kunnen ook bijdragen aan astigmatisme, wat kan voorkomen bij zowel kinderen als volwassenen.</div><div><br></div><div>Symptomen van astigmatisme manifesteren zich als wazig zicht op alle afstanden, verhoogde oogvermoeidheid bij lees- of computeractiviteiten, en zelfs hoofdpijn en oogirritatie door constante inspanning van de ogen.</div><div><br></div><div>Diagnose van astigmatisme vereist een grondig oogonderzoek, waarbij metingen van brekingsfouten worden uitgevoerd. Een cilindrische waarde wordt hierbij gebruikt om de mate van astigmatisme te kwantificeren. Geautomatiseerde instrumenten zoals een autorefractor meten de breking van het oog nauwkeurig.</div><div><br></div><div>Verschillende behandelingsmogelijkheden zijn beschikbaar. Brillen met cilindrische lenzen corrigeren astigmatisme door het licht op de juiste manier te buigen. Torische contactlenzen zijn ontworpen om ongelijke breking van licht te corrigeren. Voor permanente correctie kunnen refractieve chirurgieopties zoals Lasik en PRK worden overwogen, die de vorm van het hoornvlies veranderen.</div><div><br></div><div>Het belang van een gezonde levensstijl, inclusief regelmatige oogcontroles en goed gebruik van verlichting tijdens lezen en werken, kan niet genoeg worden benadrukt. Het bewustzijn van symptomen is cruciaal om veranderingen in het gezichtsvermogen tijdig op te sporen.</div><div><span style=\"font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);\"><br></span></div><div><ul><li><span style=\"font-size: var(--bs-body-font-size); font-weight: var(--bs-body-font-weight); text-align: var(--bs-body-text-align);\">Regelmatige oogcontroles bij een professionele oogarts zijn essentieel voor het monitoren van astigmatisme en het tijdig detecteren van eventuele veranderingen.</span></li></ul></div><div><br></div><div>In conclusie biedt een grondig begrip van astigmatisme en de beschikbare behandelingsmethoden hoop voor een helderdere toekomst met scherper zicht. Moderne optische technologieën evolueren voortdurend, waardoor patiënten effectieve oplossingen kunnen vinden voor deze veelvoorkomende oogafwijking. Een consultatie met een ervaren oogarts is essentieel voor een nauwkeurige diagnose en een passende behandelingsoptie.</div>","https://wmimages.uzleuven.be/styles/bb8d67e3da164c4556ec65b16a74c7b3c5a6622b/2020-04/astigmatisme.jpg?style=W3sianBlZyI6eyJxdWFsaXR5Ijo3NX19LHsicmVzaXplIjp7ImZpdCI6Imluc2lkZSIsIndpZHRoIjoxNDIwLCJoZWlnaHQiOjEwODAsIndpdGhvdXRFbmxhcmdlbWVudCI6dHJ1ZX19XQ==&sign=96883d2ec85be89236248aa8190ec56c4edd21247c6ead3f86fbb786bd3dcce4", ""),
			new EyeCondition("Keratoconus", "Keratoconus is een oogziekte waarbij het hoornvlies geleidelijk dunner en kegelvormig wordt, wat leidt tot wazig zicht en vervormde beelden.", "Voeg hier tekst toe","https://rockymountaineyecenter.com/wp-content/uploads/keratoconus.jpg", ""),
		};

		List<Symptom> symptoms = new()
		{
			new Symptom("Tranende ogen"),
			new Symptom("Droge ogen"),
			new Symptom("Druk in de ogen"),
		};

		conditions[0].Symptom(symptoms[2]);
		conditions[1].Symptom(symptoms[1]);
		conditions[1].Symptom(symptoms[2]);
		conditions[6].Symptom(symptoms[0]);
		dbContext.Symptoms.AddRange(symptoms);
		dbContext.EyeConditions.AddRange(conditions);
		dbContext.SaveChanges();
	}

	private void SeedPatients()
	{
		List<Patient> patients = new()
		{
			new Patient("Joe", "Doe", new DateTime(2002, 12, 5), "0495357189", "joe.doe@gmail.com"),
			new Patient("Joanna", "Doe", new DateTime(2002, 4, 23), "0474859614", "joanna.doe@gmail.com"),
		};

		dbContext.Patients.AddRange(patients);
		dbContext.SaveChanges();
	}

	private void SeedAppointments()
	{
		Patient p = new("John", "Doe", new DateTime(2002, 4, 4), "0497475147", "john.doe@gmail.com");
		Doctor d = dbContext.Employees.OfType<Doctor>().ToList()[0];

		List<Timeslot> timeslots = new()
		{
			new Timeslot(new DateTime(2023, 12, 21, 12, 0, 0))
		};
		d.Timeslot(timeslots);

		List<Appointment> appointments = new()
		{
			new Appointment(p, timeslots[0], "tranende ogen", "allergie aan x"),
			//new Appointment(
		};

		d.Appointment(appointments[0]);
		dbContext.Update(d);

		dbContext.SaveChanges();
	}

	private void SeedSymptoms()
	{
		List<Symptom> symptoms = new()
		{
			new Symptom("Rode ogen"),
			new Symptom("Wazig zicht"),
			new Symptom("Droge ogen"),
			new Symptom("Druk"),
			new Symptom("Tranende ogen")
		};
        dbContext.Symptoms.AddRange(symptoms);
        dbContext.SaveChanges();
    }

	private void SeedArticles()
	{
		List<Article> articles = new()
		{
			new Article("Bril of Contactlenzen: Het Dilemma van Optische Correctie", "Het beslissen tussen het dragen van een bril of contactlenzen is vaak een persoonlijke keuze die afhangt van individuele voorkeuren, levensstijl en medische overwegingen. Beide opties bieden uitstekende methoden voor optische correctie, maar verschillen in verschillende opzichten, van comfort en gemak tot esthetiek en onderhoud.", "<h1>Brillen: Een Stijlvolle Noodzaak</h1><p>Brillen zijn al lang niet alleen een hulpmiddel voor gezichtsvermogen, maar ook een modeaccessoire geworden. Ze komen in verschillende stijlen, monturen en kleuren, waardoor ze een persoonlijke uitdrukking van stijl en individualiteit worden. Brillen vereisen geen direct contact met de ogen, wat ze geschikt maakt voor mensen die terughoudend zijn om iets op hun ogen te plaatsen. Onderhoud is eenvoudig, maar sommigen ervaren mogelijke beperkingen bij actieve sporten of wanneer het gezichtsveld belemmerd wordt door het montuur.</p><h1>Contactlenzen: Onzichtbare Correctie</h1><p>Contactlenzen bieden een meer natuurlijke kijkervaring, omdat ze direct op het oog rusten en het gezichtsveld niet beperken zoals een brilmontuur dat kan doen. Ze zijn ideaal voor mensen met een actieve levensstijl, vooral bij sporten, en bieden vaak een beter perifeer zicht. Echter, het in- en uitdoen van contactlenzen vergt een zorgvuldige hygiëne om ooginfecties te voorkomen, en sommige mensen kunnen last hebben van droge ogen of ongemak bij langdurig dragen.</p> <h1>Wat moet je kiezen?</h1><p>Bij het kiezen tussen een bril en contactlenzen spelen verschillende factoren een rol. Persoonlijke voorkeur, dagelijkse activiteiten, ooggezondheid en het gemak van onderhoud zijn slechts enkele van de overwegingen. Een optometrist kan adviseren op basis van individuele ooggezondheid en levensstijl.</p><p>In sommige gevallen kiezen mensen voor beide opties, waarbij ze hun bril gebruiken voor dagelijks gebruik en contactlenzen voor specifieke activiteiten. Technologische vooruitgang heeft ook geleid tot lenzen die 's nachts gedragen kunnen worden voor corrigerend effect gedurende de dag.</p><h1>Conclusie</h1><p>Of het nu gaat om de keuze voor een bril of contactlenzen, beiden bieden uitstekende mogelijkheden om gezichtsvermogen te corrigeren. Het uiteindelijke besluit hangt af van individuele voorkeuren, comfort en de behoefte aan optische correctie in verschillende situaties. Een open gesprek met een optometrist kan helpen bij het nemen van een weloverwogen beslissing die past bij de levensstijl en behoeften van het individu.</p>", "https://images.unsplash.com/photo-1517948430535-1e2469d314fe?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"),
			new Article("De Voordelen van Blauw Licht Blokkerende Brillen", "In de moderne wereld worden we voortdurend blootgesteld aan schermen van computers, telefoons en andere elektronische apparaten. Blauw licht, uitgestraald door deze schermen, kan leiden tot vermoeide ogen en slaapstoornissen. Blauw licht blokkerende brillen zijn ontworpen om dit schadelijke licht te verminderen, waardoor oogcomfort wordt verbeterd en slaapkwaliteit wordt bevorderd.", "<h1>Wat is Blauw Licht?</h1><p>Blauw licht is een deel van het zichtbare lichtspectrum met een kortere golflengte en hogere energie. Elektronische apparaten zoals smartphones, computers en LED-verlichting stralen dit blauwe licht uit, waardoor het oog direct wordt blootgesteld. Langdurige blootstelling aan blauw licht kan leiden tot vermoeide, geïrriteerde ogen en mogelijk zelfs leiden tot slaapproblemen door onderdrukking van het slaaphormoon melatonine.</p><h1>Werking van Blauw Licht Blokkerende Brillen</h1><p>Blauw licht blokkerende brillen bevatten speciale lenzen die zijn ontworpen om een deel van het blauwe licht te filteren en te blokkeren dat door schermen wordt uitgezonden. Door deze brillen te dragen tijdens het gebruik van elektronische apparaten kan de hoeveelheid schadelijk blauw licht die de ogen bereikt worden verminderd. Hierdoor worden oogirritatie en vermoeidheid verminderd en kan een betere nachtrust worden bevorderd.</p><h1>Voordelen van Blauw Licht Blokkerende Brillen</h1><p>De voordelen van het gebruik van blauw licht blokkerende brillen zijn onder meer verminderde oogvermoeidheid, minder kans op droge ogen en mogelijk verbeterde slaapkwaliteit. Mensen die veel tijd doorbrengen achter schermen kunnen baat hebben bij deze brillen, vooral 's avonds, omdat ze de blootstelling aan schadelijk blauw licht verminderen.</p>", "https://images.unsplash.com/photo-1514446945-952d86c3449b?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"),
			new Article("Tips voor het Behouden van een Goede Ooggezondheid", "Een goede ooggezondheid is essentieel voor een betere kwaliteit van leven. Door eenvoudige gewoonten en voorzorgsmaatregelen kun je je ooggezondheid verbeteren en mogelijke problemen voorkomen. Het handhaven van een gezonde levensstijl en regelmatige oogcontroles zijn enkele van de belangrijke stappen om je ogen in goede conditie te houden.", "<h1>Gezonde Levensstijl</h1><p>Een uitgebalanceerd dieet dat rijk is aan voedingsstoffen zoals omega-3 vetzuren, vitamines en antioxidanten kan de ooggezondheid bevorderen. Regelmatige lichaamsbeweging en voldoende slaap zijn ook cruciaal voor het behoud van gezonde ogen. Bescherm je ogen tegen schadelijke UV-stralen door het dragen van een zonnebril met UV-bescherming wanneer je buiten bent.</p><h1>Oogverzorging en Hygiëne</h1><p>Het naleven van goede ooghygiëne, zoals het regelmatig reinigen van je handen en het vermijden van oogwrijven, helpt infecties te voorkomen. Daarnaast is het essentieel om computergebruik te onderbreken door regelmatig te knipperen en oogdruppels te gebruiken om droge ogen te voorkomen.</p><h1>Regelmatige Oogcontroles</h1><p>Laat regelmatig je ogen controleren door een optometrist of oogarts, zelfs als er geen problemen zijn. Vroege detectie van oogaandoeningen zoals glaucoom, staar of netvliesproblemen is cruciaal om de progressie ervan te vertragen en tijdig behandelingen toe te passen indien nodig.</p>", "https://images.unsplash.com/photo-1617339860632-f53c5b5dce4d?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"),
			new Article("Oogbescherming bij Sportactiviteiten", "Sporten is goed voor het lichaam, maar het kan ook risico's met zich meebrengen voor de ogen. Door oogbescherming te dragen tijdens sportactiviteiten kun je letsel aan de ogen voorkomen. Verschillende sporten vereisen verschillende soorten bescherming, dus het is belangrijk om de juiste beschermende brillen of maskers te dragen om de ogen te beschermen.", "<h1>Risico's voor de Ogen</h1><p>Elke sportactiviteit heeft zijn eigen risico's voor de ogen. Sporten zoals honkbal, tennis, squash, en hockey kunnen oogletsel veroorzaken door plotselinge impact van ballen of rackets. Contactsporten zoals boksen of rugby kunnen ook het risico op oogblessures vergroten.</p><h1>Soorten Oogbescherming</h1><p>Voor verschillende sporten zijn er verschillende soorten beschermende brillen beschikbaar. Voor balsporten zijn brillen met schokbestendige lenzen essentieel, terwijl voor watersporten een goede waterdichte bril nodig is om de ogen te beschermen tegen chemische irriterende stoffen.</p><h1>Het Belang van Oogbescherming</h1><p>Oogbescherming tijdens sportactiviteiten is essentieel om blijvende schade aan de ogen te voorkomen. Beschermende brillen kunnen het risico op verwondingen aanzienlijk verminderen en kunnen helpen bij het behouden van een goed gezichtsvermogen, zelfs na herhaalde blootstelling aan risicovolle situaties.</p>", "https://images.unsplash.com/photo-1461896836934-ffe607ba8211?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"),
			new Article("Het Belang van Oogonderzoek voor Kinderen", "Regelmatige oogonderzoeken voor kinderen zijn van vitaal belang om eventuele gezichtsproblemen in een vroeg stadium te identificeren en te behandelen. Goede visuele gezondheid is cruciaal voor de leerontwikkeling en algehele groei van kinderen. Vroege opsporing van eventuele oogproblemen kan helpen om de juiste behandelingen op tijd toe te passen en eventuele complicaties te voorkomen.", "<h1>Oogonderzoek voor Kinderen</h1><p>Veel oogproblemen bij kinderen kunnen zich voordoen zonder merkbare symptomen, waardoor regelmatige oogonderzoeken essentieel zijn. Oogtests kunnen worden uitgevoerd bij baby's, peuters en oudere kinderen om te controleren op problemen zoals bijziendheid, verziendheid, scheelzien of een lui oog.</p><h1>Voordelen van Vroege Detectie</h1><p>Vroege detectie van oogproblemen bij kinderen kan helpen om de ontwikkeling van hun visuele vaardigheden te ondersteunen en hen te helpen bij academische prestaties. Het kan ook de kans op verdere complicaties of blijvende schade aan het gezichtsvermogen verminderen door tijdige interventie en behandeling.</p><h1>Advies van Experts</h1><p>Kinderen moeten regelmatig worden gecontroleerd door een oogarts of optometrist om hun visuele gezondheid te waarborgen. Deskundig advies kan helpen bij het identificeren van eventuele problemen en het bieden van geschikte oplossingen om de visuele gezondheid van kinderen te behouden en te verbeteren.</p>", "https://images.unsplash.com/photo-1488521787991-ed7bbaae773c?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"),
		};
        dbContext.Articles.AddRange(articles);
        dbContext.SaveChanges();
    }
}